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
        link: function (scope, element, attrs) {
            element.on("save", function () {
                for (var i = 0; i < scope.components.length; i++) {
                    console.log();
                }
            });
        },
        templateUrl: '/app/components/admin/directives/post/blogPost.html'
    }
})
.directive('postComponent', function ($timeout) {
    return {
        require: '^^blogPost',
        restrict: 'E',
        link: function (scope, element, attrs, blogPostController) {
            blogPostController.addComponent(scope);
            $timeout(function () {
                element.froalaEditor();
            });
        },        
        templateUrl: function (element, attr) {
            return '/app/components/admin/directives/post/components/' + element.attr("component-type") + "Component.html"
        }
    }
});