angular.module("umbraco").controller("MediaReportingController", function ($scope, $location, entityResource, localizationService, mediaReportingResource) {

    var vm = this;
		vm.loading = false;
		vm.labels = {};
		vm.filters = {
			status: {
				open: false,
				options: [
					{ id: 0, name: "Active", selected: false, color: "success" },
					{ id: 1, name: "Deleted", selected: false, color: "warning" },
				]
			},
			user:  {
				open: false,
				options: []
			},
			type:  {
				open: false,
				options: []
			},
		};	
		vm.size = null;
		vm.search = "";

		vm.tableResults = [];
		vm.paging = { pageIndex: 0, pageSize: 25 };


		const init = () => {
			vm.loading = true;

			localizationService.localizeMany(["user_stateAll"]).then(data => {
					vm.labels.all = data[0];
			});
			
			entityResource.getAll("User")
				.then(res => {
					vm.filters.user.options = res.map(usr => ({ 
						id: usr.id, 
						name: usr.name, 
						selected: false 
					}));
				});

			mediaReportingResource.getMediaTypes()
				.then(res => {
					vm.filters.type.options = res.map(type => ({ 
						id: type.id, 
						name: type.name, 
						alias: type.alias, 
						icon: type.icon,
						selected: false 
					}));
				});

			vm.loading = false;

			searchMedia(0);
		}


		const searchMedia = (pageIndex) => {
			// It's easier to clear the results to scroll to the top of the table
			vm.tableResults = [];
			mediaReportingResource.searchMedia(
				vm.search,
				{
					creatorIds: vm.filters.user.options.filter(x => x.selected === true)?.map(x => x.id),
					mediaStatus: vm.filters.status.options.filter(x => x.selected === true)?.map(x => x.id),
					mediaTypeIds: vm.filters.type.options.filter(x => x.selected === true)?.map(x => x.id),
					minimumSize: vm.size
				}, 
				{ 
					pageIndex: pageIndex, 
					pageSize: 25
				}
			)
			.then(response => {
				vm.tableResults = response.items;
				vm.paging = response.paging;
			})
		}


		$scope.$watch("vm.size", _.debounce((newVal, oldVal) => {
			if (newVal !== oldVal) {
				searchMedia(0);
			}
		}, 500));

		$scope.$watch("vm.search", _.debounce((newVal, oldVal) => {
			if (newVal !== oldVal) {
				searchMedia(0);
			}
		}, 500));

		$scope.$watch("vm.filters", _.debounce((newVal, oldVal) => {
			if (newVal !== oldVal) {
				searchMedia(0);
			}
		}, 500), true);


		vm.toggleFilter = (filterName) => {
			Object.keys(vm.filters).forEach(filter => {
				vm.filters[filter].open = false;
			});
			if (vm.filters[filterName] !== undefined) {
				vm.filters[filterName].open = true;
			}
		}


		vm.getFilterName = (filterName) => {
			var name = vm.labels.all;
			var multiple = false;
			if (vm.filters[filterName] !== undefined) {
				vm.filters[filterName].options.forEach(option => {
					if (option.selected) {
						if (!multiple) {
							name = option.name
							multiple = true;
						} else {
							name = name + ", " + option.name;
						}
					}
				});
			}
			return name;
		}


		vm.changePageNumber = (pageNumber) => {
			searchMedia(pageNumber - 1);
		}


		vm.formatBytes = (bytes, decimals = 2) => {
			if (!+bytes) {
				return '0 Bytes';
			}
			const k = 1024
			const dm = decimals < 0 ? 0 : decimals
			const sizes = ['Bytes', 'KiB', 'MiB', 'GiB', 'TiB', 'PiB', 'EiB', 'ZiB', 'YiB']
			const i = Math.floor(Math.log(bytes) / Math.log(k))
			return `${parseFloat((bytes / Math.pow(k, i)).toFixed(dm))} ${sizes[i]}`
		}


		vm.clickUser = (user, $event) => {
			$event.stopPropagation();
			$event.preventDefault();
			$location
				.path(`/users/users/user/${user.id}`)
				.search("dashboard", null);
		}


		vm.clickMedia = (mediaItem, $event) => {
			$event.stopPropagation();
			$event.preventDefault();
			console.log(mediaItem);
			$location
				.path(`/media/media/edit/${mediaItem.id}`)
				.search("dashboard", null);
		}


		vm.exportResults = () => {
			mediaReportingResource.exportResults(
				vm.search,
				{
					creatorIds: vm.filters.user.options.filter(x => x.selected === true)?.map(x => x.id),
					mediaStatus: vm.filters.status.options.filter(x => x.selected === true)?.map(x => x.id),
					mediaTypeIds: vm.filters.type.options.filter(x => x.selected === true)?.map(x => x.id),
					minimumSize: vm.size
				}
			)
			.then(reportData => {
				var attachment = document.createElement("a");
				attachment.href = "data:attachment/csv," + encodeURIComponent(reportData);
				attachment.download = "mediaReport.csv";
				attachment.click();
			})
		}

	
		init();
});