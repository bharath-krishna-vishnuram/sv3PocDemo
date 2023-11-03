
$(function () {
    var l = abp.localization.getResource('MvcDemoApplication');
    var createModal = new abp.ModalManager(abp.appPath + 'PDM/Structures/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'PDM/Structures/UpdateModal');

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewStructureButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    var dataTable = $('#StructuresTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(trial.mvcDemoApplication.pDM.structure.getList),
            columnDefs: [
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('Type'),
                    data: "type",
                    render: function (data) {
                        return l('Enum:StructureType.' + data);
                    }
                },
                {
                    title: l('Description'),
                    data: "description"
                },
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
                                    text: l('View Hierarchy'),
                                    action: function (data) {
                                        location.href = "/PDM/Structures/ViewHierarchy?id=" + data.record.id;
                                    }
                                },
                                {
                                    text: l('Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    confirmMessage: function (data) {
                                        return l('DeletionConfirmationMessage:Structure', data.record.name);
                                    },
                                    action: function (data) {
                                        trial.mvcDemoApplication.pDM.structure
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
