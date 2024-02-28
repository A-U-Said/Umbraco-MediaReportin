angular.module("umbraco").controller("MediaReportingController", function ($scope, entityResource, localizationService, mediaReportingResource) {

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
		}


		$scope.$watch("vm.size", _.debounce((newVal, oldVal) => {
			if (newVal !== oldVal) {
				console.log(vm.size);
			}
		}, 500));

		$scope.$watch("vm.search", _.debounce((newVal, oldVal) => {
			if (newVal !== oldVal) {
				console.log(vm.search);
			}
		}, 500));

		$scope.$watch("vm.filters", _.debounce((newVal, oldVal) => {
			if (newVal !== oldVal) {
				console.log(vm.filters);
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

	
		init();
});