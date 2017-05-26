(function () {
    'use strict';
    angular.module('app')
    .controller('orderController', orderController);

    orderController.$inject = ['dataService', 'configService', '$state'];

    function orderController(dataService, configService, $state) {
        var apiUrl = configService.getApiUrl();
        var vm = this;

        //Propiedades
        vm.order = {};
        vm.orderList = [];
        vm.modalTitle = '';
        vm.modalButtonTitle = '';
        vm.readOnly = "";
        vm.isDelete = "";
        vm.idOrder = 0;

        //Funciones
        vm.create = create;
        vm.edit = edit;
        vm.getOrder = getOrder;
        vm.delete = delet;

        init();

        function init() {
            if (!configService.getLogin()) return $state.go('login');
            list();
        }

        function list() {
            dataService.getData(apiUrl + '/order/list')
            .then(
            function (result) {
                vm.orderList = result.data;
            },
            function (error) {
                vm.orderList = [];
                console.log(error);
            });
        }

        function getOrder(id) {
            vm.order = null;
            dataService.getData(apiUrl + '/order/' + id)
            .then(
                function (result) {
                    vm.order = result.data;
                    vm.idOrder = id;
                },
                function (error) {
                    console.log(error);
                }
            );
        }

        function closeModal() {
            angular.element("#modal-container").modal('hide');
        }

        function edit() {
            vm.modalTitle = 'Editar Orden';
            vm.modalButtonTitle = 'Actualizar';
            vm.readOnly = false;
            vm.isDelete = false;
            vm.modalFunction = updateOrder;
        }

        function updateOrder() {
            if (!vm.order) return;
            dataService.putData(apiUrl + '/order', vm.order)
            .then(
                function (result) {
                    vm.order = {};
                    list();
                    closeModal();
                },
                function (error) {
                    console.log(error);
                });
        }

        function create() {
            vm.order = {};
            vm.modalTitle = 'Crear Nueva Orden';
            vm.modalButtonTitle = 'Crear';
            vm.readOnly = false;
            vm.isDelete = false;
            vm.modalFunction = createOrder;
        }

        function createOrder() {
            if (!vm.order) return;
            dataService.postData(apiUrl + '/order', vm.order)
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

        function delet() {
            vm.modalTitle = 'Eliminar Orden';
            vm.modalButtonTitle = 'Eliminar';
            vm.readOnly = true;
            vm.isDelete = true;
            vm.modalFunction = deleteOrder;
        }

        function deleteOrder() {
            if (!vm.order) return;
            dataService.deleteData(apiUrl + '/order/' + vm.idOrder)
            .then(
                function (result) {
                    vm.order = {};
                    vm.idOrder = 0;
                    list();
                    closeModal();
                },
                function (error) {
                    console.log(error);
                });
        }
    }
})();