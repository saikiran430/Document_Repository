function openEditModal(id, name, code) {
    document.getElementById('editDocumentId').value = id;
    document.getElementById('editDocumentName').value = name;
    document.getElementById('editDocumentCode').value = code;
    var modal = new bootstrap.Modal(document.getElementById('editModal'));
    modal.show();
}

function openDeleteModal(id) {
    document.getElementById('deleteDocumentId').value = id;
    var modal = new bootstrap.Modal(document.getElementById('deleteModal'));
    modal.show();
}
