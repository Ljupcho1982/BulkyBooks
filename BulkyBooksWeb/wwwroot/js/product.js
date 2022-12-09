var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {

    dataTable = $('#tblData').DataTable({

        "ajax": {

            "url":"/Admin/Product/GetAll"
        },
        "columns": [

            { "data": "title", "width": "15%" },

            { "data": "isnb", "width": "15%" },

            { "data": "price", "width": "15%" },
            { "data": "author", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return ` <div class="w-75 btn-group" role="group">

                        <a href="/Admin/Product/Upsert?id=${data}"><i class="bi bi-pencil"></i>Edit</a>
                        <a onClick=Delete('/Admin/Product/Delete/+${data}')>Delete</a>
                        <a href="/Admin/Product/Upsert?id=${data}"><i class="bi bi-trash"></i>Delete</a>
                    </div>
`

                } ,
                "width":"15%"

            }
           

     
        ]

    });
};

function Delete() {

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'No, cancel!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            swalWithBootstrapButtons.fire(
                $.ajax({
                    url: url, type: 'DELETE',
                    success: function (data) {

                        if (data.success) {

                            dataTable.ajax.reload();
                        }
                    }


                })
            )
        } else if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire(
                'Cancelled',
                'Your imaginary file is safe :)',
                'error'
            )
        }
    })
}