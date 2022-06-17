var dataTable;
var l;
$(function () {
    l = abp.localization.getResource('EmailMaketing');
    var createModal = new abp.ModalManager(abp.appPath + 'Customers/CreateModal');
    /*var editModal = new abp.ModalManager(abp.appPath + 'Customers/EditModal');*/

    dataTable = $('#CustomerTable').DataTable(
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
                {
                    title: l('User Name'),
                    data: "userName"
                },
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
                    "orderable": false,
                    title: l('Status'),
                    data: { status: "status", id: "id" },
                    render: function (data) {

                        var check = '';
                        if (data.status == 1)
                            check = "checked";
                        var str = '<label class="switch">' +
                            `<input type = "checkbox" id="a${data.id}" ${check} onclick="ChangeStatus(this.id,${data.status})">` +
                            '<span class="slider round"></span>' +
                            '</label >';
                        return str;

                    }
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


    $('#NewCustomerButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
   
});
function ChangeStatus(id, status) {
    if ($('#a' + id).is(':checked')) {
        $("#a" + id).prop("checked", false);
    }
    else {
        $("#a" + id).prop("checked", true);
    }
    dataTable.ajax.reload();
    var mess = l('Block The User');
    if (status == 0) {
        mess = l('Unlock The User');
    }
    console.log(id.substring(1))
    abp.message.confirm(mess, l('Notify'))
        .then(function (confirmed) {

            if (confirmed) {
                emailMaketing.customers.customer.changeStatus(id.substring(1))
                abp.message.success(l('Successfully'), l('Congratulations'));

                if ($('#a' + id).is(':checked')) {
                    $("#a" + id).prop("checked", false);
                }
                else {
                    $("#a" + id).prop("checked", true);
                }
                dataTable.ajax.reload();
            }
            else {
                if ($('#a' + id).is(':checked')) {
                    $("#a" + id).prop("checked", false);
                }
                else {
                    $("#a" + id).prop("checked", true);
                }
            }
        });
};
