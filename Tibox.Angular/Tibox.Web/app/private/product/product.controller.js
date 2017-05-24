(function () {
    'use strict';
    angular.module('app')
    .controller('productController', productController);

    productController.$inject = ['dataService', 'configService', '$state'];

    function productController(dataService, configService, $state) {
        var apiUrl = configService.getApiUrl();
        var vm = this;

        //Propiedades
        vm.product = {};
        vm.productList = [];
        vm.modalTitle = '';
        vm.modalButtonTitle = '';
        vm.readOnly = false;
        vm.isDelete = false;

        //Funciones
        vm.create = create;
        vm.edit = edit;
        vm.getProduct = getProduct;

        init();

        function init() {
            if (!configService.getLogin()) return $state.go('login');
            list();
        }

        function list() {
            dataService.getData(apiUrl + '/product/list')
            .then(
            function (result) {
                vm.productList=result.data;
            },
            function (error) {
                vm.productList = [];
                console.log(error);
            });
        }

        function getProduct(id) {
            vm.product = null;
            dataService.getData(apiUrl + '/product/' + id)
            .then(
                function (result) {
                    vm.product = result.data;
                },
                function (error) {
                    console.log(error);
                }
            );
        }

        function closeModal() {
            angular.element('#modal-container').modal('hide');
        }

        function edit() {
            vm.modalTitle = 'Editar Producto';
            vm.modalButtonTitle = 'Actualizar';
            vm.readOnly = false;
            vm.isDelete = false;
            vm.modalFunction = updateProduct;
        }
        
        function updateProduct() {
            if (!vm.product) return;
            dataService.putData(apiUrl + '/product', vm.product)
            .then(
                function (result) {
                    vm.product = {};
                    list();
                    closeModal();
                },
                function (error) {
                    console.log(error);
                });
        }
               

        function create() {
            vm.product = {};
            vm.modalTitle = 'Crear Nuevo Producto';
            vm.modalButtonTitle = 'Crear';
            vm.readOnly = false;
            vm.isDelete = false;
            vm.modalFunction = createProduct;
        }

        function createProduct() {
            if (!vm.product) return;
            dataService.postData(apiUrl + '/product', vm.product)
            .then(
                function (result) {
                    list();
                    closeModal();
                },
                function (error) {
                    console.log(error);
                }
            );
        }
    }


})();