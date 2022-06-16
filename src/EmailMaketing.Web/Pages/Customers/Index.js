$(function () {
    var l = abp.localization.getResource('EmailMaketing');
    /*var createModal = new abp.ModalManager(abp.appPath + 'Customers/CreateModal');*/
    /*var editModal = new abp.ModalManager(abp.appPath + 'Customers/EditModal');*/

    var dataTable = $('#CustomerTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(emailMaketing.customers.customer.getList),
            columnDefs: [
               /* {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('BachHoaXanh.Customers.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('BachHoaXanh.Customers.Delete'),
                                    confirmMessage: function (data) {
                                        return l('CustomerDeletionConfirmationMessage', data.record.name);
                                    },
                                    action: function (data) {
                                        bachHoaXanh.customers.customer
                                            .delete(data.record.id)
                                            .then(function (data) {
                                                if (data) {
                                                    abp.notify.info(l('SuccessfullyDeleted'));
                                                    dataTable.ajax.reload();
                                                } else {
                                                    abp.message.error(l("NotifyDeleteBill"));
                                                }

                                            });
                                    }
                                }
                            ]
                    }
                },*/
               /* {
                    title: l('User Name'),
                    data: "userName"
                },*/
                {
                    title: l('Name'),
                    data: "fullName",
                },
                {
                    title: l('Phone Number'),
                    data: "phoneNumber",

                },
                {
                    title: l('Email'),
                    data: "email"
                },
                {
                    title: l('CreationTime'), data: "creationTime",
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString(luxon.DateTime.DATETIME_SHORT);
                    }
                }
            ]
        })
    );
    /*createModal.onResult(function () {
        dataTable.ajax.reload();
    });
*/
    /*editModal.onResult(function () {
        dataTable.ajax.reload();
    });*/


    /*$('#NewCustomerButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });*/
});
