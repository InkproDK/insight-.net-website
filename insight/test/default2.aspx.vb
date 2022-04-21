Imports System.Data.SqlClient
Public Class _default1
    Inherits System.Web.UI.Page

    Dim objSQL01Conn As New SqlConnection(ConfigurationManager.ConnectionStrings("sql01conn").ConnectionString)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Response.Write(GetSplitOrdersTotal)


        Dim strSqlCmdGetAPILogins As SqlCommand = objSQL01Conn.CreateCommand
        strSqlCmdGetAPILogins.CommandText =
            "SELECT COUNT(*) As Antal, CASE WHEN [Currency Code] = '' THEN 'DKK' ELSE [Currency Code] END AS 'OrdersPerCountry'
            FROM [BC18_InkPro].[dbo].[Inkpro$Sales Header$437dbf0e-84ff-417a-965d-ed2bb9650972] WHERE [Document Type] = 1 AND [Sell-to Customer No_] <> '888'
            GROUP BY [Currency Code]"
        objSQL01Conn.Open()

        Dim strSqlReader As SqlDataReader = strSqlCmdGetAPILogins.ExecuteReader()
        Try
            Response.Write("Antal" & ", Land<br>")

            Dim iSumOrders As Integer = 0

            Dim strArr As String = ""
            Dim strDivider As String = ""
            Dim strBuild As String = ""

            Dim i As Integer = 0

            While strSqlReader.Read()

                'Call APILogin function
                'Response.Write("User name: " & strSqlReader("Username") & "<br>" & "Ordrenr.: " & strSqlReader("[Source No_]") & "<br><br>")
                'Response.Write(strSqlReader("Antal") & ", " & strSqlReader("OrdersPerCountry") & "<br>")
                'iSumOrders += strSqlReader("Antal")

                If i < 2 Then
                    strDivider = ", "
                Else
                    strDivider = ""
                End If

                strBuild += strSqlReader("Antal") & strDivider

                i += 1

            End While

            strArr = "[" & strBuild & "]"
            Response.Write(strArr)

        Catch ex As Exception
            Response.Write(ex.ToString())
        End Try

        objSQL01Conn.Close()
    End Sub

    Protected Function GetSplitOrdersTotal()

        Dim intGetSplitOrdersTotal As Integer = 0
        Dim SqlCmdGetSplitOrdersTotal As SqlCommand = objSQL01Conn.CreateCommand
        SqlCmdGetSplitOrdersTotal.CommandText =
            "SELECT COUNT(DISTINCT sl.[Document No_])
            FROM [Inkpro$Sales Line$437dbf0e-84ff-417a-965d-ed2bb9650972] sl
	            INNER JOIN(
		            SELECT [Document No_], COUNT(DISTINCT [Document No_]) AS colCount
		            FROM [BC18_InkPro].[dbo].[Inkpro$Sales Line$437dbf0e-84ff-417a-965d-ed2bb9650972]
		            WHERE [Document Type] = 1 AND [Sell-to Customer No_] <> '888'
		    GROUP BY [Document No_] HAVING SUM(Quantity) - SUM([Quantity Shipped]) > 0 AND NOT SUM([Quantity Shipped]) = 0) as cnt ON sl.[Document No_] = cnt.[Document No_]"

        If Not objSQL01Conn.State = ConnectionState.Open Then
            objSQL01Conn.Open()
        End If

        intGetSplitOrdersTotal = SqlCmdGetSplitOrdersTotal.ExecuteScalar()
        objSQL01Conn.Close()

        Return intGetSplitOrdersTotal

    End Function


End Class