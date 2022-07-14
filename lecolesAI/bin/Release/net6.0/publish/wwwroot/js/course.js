var dataTable;

$(document).ready(function () {
    loadDataTable();
   
});

function loadDataTable() {

	dataTable = $('#tblData').DataTable({
		"ajax": {
			"url": "Lecoles/GetCourses"


		},

		"columns": [
			{ "data": "course_title", "width": "15%" },
			{ "data": "price", "width": "15%",render: function (data, type, full, meta) { return "$" + data }},
			{ "data": "subject", "width": "15%" },
			{
				"data": "index",
				"render": function (data) {
					return `
                        <div class="w-75 btn-group" role="group">
                        <a href="Lecoles/Edit/${data}"
						
                        class="btn btn-primary mx-2"> <i class="bi bi-zoom-in"></i>Similar </a>

						
				
                   
					</div>
                        `
				},
				"width": "15%"
			}
			
			
	    
	    
	    
	    ]
	
	
	
	
	});

}