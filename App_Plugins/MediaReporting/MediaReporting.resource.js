angular.module('umbraco.resources').factory('mediaReportingResource', function($q, $http, umbRequestHelper) {

  const baseUrl = "MediaReporting/MediaReporting";

  const buildQueryStringFromArray = (paramName, arr, appendAmp) => {
    if (!arr || arr.length === 0) {
      return "";
    }
    var qs = "";
    for (var i = 0; i < arr.length; i++) {
      qs += `${paramName}=${arr[i]}`;
      if (arr[i+1] != null) {
        qs += "&";
      }
    }
    if (appendAmp === true) {
      qs += "&";
    }
    return qs;
  }

  return {

    getMediaTypes: () => {
      return umbRequestHelper.resourcePromise(
        $http.get(`${baseUrl}/GetMediaTypes`),
        `Failed to get media types`
      );
    },
		
    searchMedia: (searchTerm, filter, paging) => {
      var qs = "";
      qs += buildQueryStringFromArray("creatorIds", filter.creatorIds, true);
      qs += buildQueryStringFromArray("mediaStatus", filter.mediaStatus, true);
      qs += buildQueryStringFromArray("mediaTypeIds", filter.mediaTypeIds, true);
      qs += filter.minimumSize ? `minimumSize=${filter.minimumSize}&` : "&";
      qs += searchTerm ? `searchTerm=${searchTerm}&` : "";
      qs += umbRequestHelper.dictionaryToQueryString([
        { pageIndex: paging.pageIndex },
        { pageSize: paging.pageSize }
      ]);
      return umbRequestHelper.resourcePromise(
        $http.get(`${baseUrl}/GetAllMediaSizes?${qs}`),
        `Failed to search media`
      );
    },

  }

}); 