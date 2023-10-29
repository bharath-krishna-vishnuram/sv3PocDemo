$(function () {
    var l = abp.localization.getResource('MvcDemoApplication');
    var createModal = new abp.ModalManager(abp.appPath + 'PDM/TextManager/CreateText');
    var editModal = new abp.ModalManager(abp.appPath + 'PDM/TextManager/UpdateText');

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewTextButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    var dataTable = $('#TextsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(trial.mvcDemoApplication.pDM.text.getList),
            columnDefs: [
                
                {
                    title: l('Name'),
                    data: "textName"
                },
                {
                    title: l('ColumnHeaders:UniqueId'),
                    data: "uniqueTextId"
                },
                {
                    title: l('ColumnHeaders:IsStructure'),
                    data: "isStructure"
                },
                {
                    title: l('ColumnHeaders:IsComponent'),
                    data: "isComponent"
                },
                {
                    title: l('ColumnHeaders:IsDescriptor'),
                    data: "isDescriptor"
                },
                {
                    title: l('ColumnHeaders:IsOption'),
                    data: "isOption"
                },
                {
                    title: l('ColumnHeaders:IsDeleted'),
                    data: "isDeleted"
                },
                //{
                //    title: l('Type'),
                //    data: "type",
                //    render: function (data) {
                //        return l('Enum:BookType.' + data);
                //    }
                //},
                //{
                //    title: l('PublishDate'),
                //    data: "publishDate",
                //    render: function (data) {
                //        return luxon
                //            .DateTime
                //            .fromISO(data, {
                //                locale: abp.localization.currentCulture.name
                //            }).toLocaleString();
                //    }
                //},
                //{
                //    title: l('Price'),
                //    data: "price"
                //},
                {
                    title: l('ColumnHeaders:CreationTime'), data: "creationTime",
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString(luxon.DateTime.DATETIME_SHORT);
                    }
                },
                {
                    title: l('ColumnHeaders:LastUpdateTime'), data: "lastModificationTime",
                    render: function (data) {
                        if (data == null)
                            return "NA"
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString(luxon.DateTime.DATETIME_SHORT);
                    }
                },
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    confirmMessage: function (data) {
                                        return l('DeletionConfirmationMessage:Text', data.record.textName);
                                    },
                                    action: function (data) {
                                        trial.mvcDemoApplication.pDM.text
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(l('SuccessfullyDeleted'));
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                }
            ]
        })
    );
});
