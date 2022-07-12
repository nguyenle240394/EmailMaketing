var dataTable;
var l;
$(function () {
    l = abp.localization.getResource('EmailMaketing');
    /*var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'Customers/CreateModal',
        scriptUrl: '/Pages/Customers/CreateCusotmer.js'
    });*/

    /*viewUrl: abp.appPath + 'Categories/CreateModal',
        scriptUrl : '/Pages/Categories/Create.js'*/
    /*var editModal = new abp.ModalManager(abp.appPath + 'Customers/EditModal');*/

    dataTable = $('#LetterTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(emailMaketing.contentEmails.contentEmail.getList),
            columnDefs: [
               /* {
                    title: l('STT'),
                    data: "stt"
                },*/
                {
                    title: l('User'),
                    data: "customerName"
                },
                {
                    title: l('Email'),
                    data: "senderEmail",
                },
                {
                    title: l('Subject'),
                    data: "subject",

                },
                {
                    title: l('Body'),
                    data: "bodyShow"
                },
                {
                    title: l('Creation Time'), data: "creationTime",
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString(luxon.DateTime.DATETIME_SHORT);
                    }
                },
            ]
        })
    );
   /* editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    resetPasswordModal.onResult(function () {
        dataTable.ajax.reload();
    });


    $('#NewCustomerButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
    $('#Customer_FullName').keypress(function (e) {
        console.log(e);
    });
*/
});