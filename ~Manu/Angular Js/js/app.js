 //1. create app module 
        var trainingApp = angular.module('trainingApp', []);

        //2. create controller
        trainingApp.controller("trainingController", function ($scope, $http) {
  
            //3. attach originalTrainging model object
            $scope.originalTrainging = {
                trainingName: '',
                startDate: new Date(),
				endDate: new Date()
            };

            //4. copy originalTrainging to trainging. trainging will be bind to a form 
            $scope.trainging = angular.copy($scope.originalTrainging);

            //5. create submitTraingingForm() function. This will be called when user submits the form
            $scope.submitTrainingForm = function () {
			
			if( $scope.trainging.startDate>$scope.trainging.endDate){
				$scope.details = "End date is before the start date";
			}
			else{
				$http({
				     method: 'POST',
					 url: 'http://localhost:55371/api/training',
					 data: $scope.trainging,
					 headers: {'Content-Type': 'application/x-www-form-urlencoded'}
					 }).then(
					  function successCallback(response) {
						$scope.details = response.data.message;
						$scope.difference = response.data.diferrence;
					  },
					  function errorCallback(response) {
						$scope.details="Unable to perform request";
					  }
					);
				}
			};
		});