
$(function () {
    l = abp.localization.getResource('EmailMaketing');
    var createModal = new abp.ModalManager(abp.appPath + 'SenderEmails/CreateModal');
    //getFilter = function () {
    //    return {
    //        filterText: $("input[name='Search']").val()
    //    }
    //}
    var dataTable = $('#SenderEmailTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(emailMaketing.senderEmails.senderEmail.getListWithNavigation),
            columnDefs: [
                //{
                //    title: l('Action'),
                //    rowAction: {
                //        items: [
                //            {
                //                text: l('Edit'),
                //                action: function (data) {
                //                    editModal.open({ id: data.record.id})
                //                }
                //            }
                //        ]
                //    }
                //},
                {
                    title: l('Email'),
                    data: "senderEmail.email",
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
                }
            ]

        })
    )
    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    //editModal.onResult(function () {
    //    dataTable.ajax.reload();
    //});

    $('#NewSenderEmailButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});