
$(function () {
    l = abp.localization.getResource('EmailMaketing');
    var createModal = new abp.ModalManager(abp.appPath + 'EmailManagement/SenderEmails/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'EmailManagement/SenderEmails/EditModal');

    var dataTable = $('#SenderEmailTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(emailMaketing.senderEmails.senderEmail.getListWithNavigation),
            columnDefs: [
                {
                    title: l('STT'),
                    data: "stt"
                },
                {
                    title: l('Email'),
                    data: "senderEmail.email"
                },
                {
                    title: l('Password'),
                    data: "senderEmail.password"
                },
                {
                    title: l('Customer Name'),
                    data: "customer",
                    render: function (data) {
                        if (data != null) return data.fullName;
                        return "";
                    }
                },
                {
                    title: l('IsSend'),
                    data: "senderEmail.isSend"
                },
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    iconClass: "fa fa-pencil-square-o",
                                    visible: abp.auth.isGranted('EmailMaketing.SenderEmails.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.senderEmail.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    iconClass: "fa fa-trash-o",
                                    visible: abp.auth.isGranted('EmailMaketing.SenderEmails.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'Sender Email Deletion Confirmation Message',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        emailMaketing.senderEmails.senderEmail
                                            .delete(data.record.senderEmail.id)
                                            .then(function (data) {
                                                if (data) {
                                                    abp.notify.info(l('Successfully Deleted'));
                                                    dataTable.ajax.reload();
                                                } else {
                                                    abp.message.error(l("Delete Failed"));
                                                }
                                            });
                                    }
                                }
                            ]
                    }
                }
            ]

        })
    )
    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewSenderEmailButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});

$(function () {
    $(document).ready(function () {
        $('input[type="file"]').change(function (e) {
            var fileName = e.target.files[0].name;
            if (fileName != null) {
                $('#ImportExcelButton').reload(document.getElementById("ImportExcelButton").disabled = false);
            }
        });
    });
})