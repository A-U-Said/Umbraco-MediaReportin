angular.module('umbraco.resources').factory('mediaReportingResource', function($q, $http, umbRequestHelper) {

  const baseUrl = "MediaReporting/MediaReporting";

  return {

    getMediaTypes: () => {
      return umbRequestHelper.resourcePromise(
        $http.get(`${baseUrl}/GetMediaTypes`),
        `Failed to get media types`
      );
    },
		
		

  }

}); 