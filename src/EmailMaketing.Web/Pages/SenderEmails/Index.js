
$(function () {
    l = abp.localization.getResource('EmailMaketing');
    //var createModal = new abp.ModelManager({
    //    viewUrl: abp.abpPath + 'SenderEmails/CreateModal',
    //    scriptUrl: '/Pages/SenderEmails/Create.js'
    //});
    //getFilter = function () {
    //    return {
    //        filterText: $("input[name='Search']").val()
    //    }
    //}
    dataTable = $('#SenderEmailTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(emailMaketing.senderEmails.senderEmail.getList),
            columnDefs: [
                //{
                //    title: l('Action'),
                //    rowAction
                //},
                {
                    title: l('Email'),
                    data: "email"
                },
                {
                    title: l('Password'),
                    data: "password"
                },
                {
                    title: l('CustomerName'),
                    data: "customerName"
                },
                {
                    title: l('IsSend'),
                    data: "isSend"
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
        //createModal.open();
    });
});