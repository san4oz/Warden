adminApp.directive('blogPost', function () {
    return {
        restrict: 'E',
        transclude: true,
        controller: ['$scope', function BlogPostController($scope) {
            var components = $scope.components = [];

            this.addComponent = function (component) {
                components.push(component);
            };
        }],
        templateUrl: '/app/components/admin/directives/post/blogPost.html'
    }
})
    .directive('postComponent', function ($timeout, $interpolate, $rootScope) {
    return {
        require: ['^^blogPost', 'ngModel'],
        restrict: 'E',          
        link: function (scope, element, attrs, references) {

            var ngModel = references[1];
            var blogPostController = references[0];

            blogPostController.addComponent(scope);
            $timeout(function () {
                element.froalaEditor();
            });
           
            function read() {
                ngModel.$setViewValue(element.froalaEditor('html.get'));
            }

            ngModel.$render = function () {
                element.html(ngModel.$viewValue || "");
            };

            element.bind("blur", function () {
                scope.$apply(read);
            });
            element.bind("keydown keypress", function (event) {
                if (event.which === 13) {
                    this.blur();
                    event.preventDefault();
                }
            });

            scope.componentTemplate = "/app/components/admin/directives/post/components/" + attrs.ctype + "Component.html";
        },      
        template: "<div ng-include='componentTemplate'></div>"     
    }
});