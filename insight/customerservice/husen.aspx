<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="husen.aspx.vb" Inherits="insight.husen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<link href="https://fonts.googleapis.com/css?family=Lato:300,400,400i,700,700i,900" rel="stylesheet" />
<link rel="stylesheet" href="https://pro.fontawesome.com/releases/v5.1.0/css/all.css" integrity="sha384-87DrmpqHRiY8hPLIr7ByqhPIywuSsjuQAfMXAE0sMUpY3BM7nXjf+mLIUSvhDArs" crossorigin="anonymous" />
<link rel="stylesheet" href="https://s3-eu-west-1.amazonaws.com/inkprostatic/inhouse+tools/main.css" />
    <link rel="stylesheet" type="text/css" href="//cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.1.1/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="//cdn.datatables.net/1.10.19/css/dataTables.bootstrap4.min.css" />
    <script src="//code.jquery.com/jquery-3.3.1.js" type="text/javascript"></script>
    <script src="//cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="//cdn.datatables.net/1.10.19/js/dataTables.bootstrap4.min.js" type="text/javascript"></script>
    <script>
        $(document).ready(function() {
            $('#tblproducts').DataTable({
                "order": [[ 5, "asc" ]],
                "language": {
                    "lengthMenu": "Vis _MENU_ resultater pr. side",
                    "zeroRecords": "Ingen resultater",
                    "info": "Viser side _PAGE_ af _PAGES_",
                    "infoEmpty": "Ingen resultater",
                    "infoFiltered": "(filtreret fra _MAX_ resultater i alt)",
                    "search": "Søg:",
                    "decimal": ",",
                    "paginate": {
                        "first": "Første",
                        "last": "Sidste",
                        "next": "Næste",
                        "previous": "Forrige"
                    },
                }

            }

                );
        });
    </script>
<style>

html {
    overflow-y:scroll;
}

.maindiv {
    padding: 20px 20px 20px 20px;
}

.wrapper {
    max-width:1200px
}

.header *, .header *::before, .header *::after {
	box-sizing: unset;
}

.dataTables_filter input {
    width:400px !important;
}

</style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="maindiv">
            <asp:Label ID="lblSearchResults" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
