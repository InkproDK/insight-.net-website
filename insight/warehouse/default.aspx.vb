Imports System.Data.SqlClient
Public Class _default2
    Inherits System.Web.UI.Page

    Dim objSQL01Conn As New SqlConnection(ConfigurationManager.ConnectionStrings("sql01conn").ConnectionString)

    Public strPieChartData As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim strSqlCmdGetAPILogins As SqlCommand = objSQL01Conn.CreateCommand
        strSqlCmdGetAPILogins.CommandText =
            "SELECT COUNT(*) As Antal, CASE WHEN [Currency Code] = '' THEN 'DKK' ELSE [Currency Code] END AS 'OrdersPerCountry'
            FROM [BC18_InkPro].[dbo].[Inkpro$Sales Header$437dbf0e-84ff-417a-965d-ed2bb9650972] WHERE [Document Type] = 1 AND [Sell-to Customer No_] <> '888'
            GROUP BY [Currency Code]"

        If Not objSQL01Conn.State = ConnectionState.Open Then
            objSQL01Conn.Open()
        End If

        Dim strSqlReader As SqlDataReader = strSqlCmdGetAPILogins.ExecuteReader()
        Try

            Dim arrValues As List(Of Integer) = New List(Of Integer)


            While strSqlReader.Read()

                arrValues.Add(strSqlReader("Antal"))

            End While

            strPieChartData = "[" & String.Join(", ", arrValues.ToArray) & "]"

        Catch ex As Exception
            Response.Write(ex.ToString())
        End Try

        objSQL01Conn.Close()

    End Sub

End Class