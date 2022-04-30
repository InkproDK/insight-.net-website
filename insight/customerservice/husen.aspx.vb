Imports System.Data.SqlClient
Public Class Husen
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objSQL01Conn As New SqlConnection(ConfigurationManager.ConnectionStrings("sql01conn").ConnectionString)

        Dim SqlCmd As SqlCommand = objSQL01Conn.CreateCommand
        SqlCmd.CommandText = "SELECT DISTINCT item.No_ As 'ProductId', item.[Description] As 'Title', CAST(ISNULL(SUM$Quantity, 0) AS bigint) AS 'InStock', 
                                (CAST(ISNULL(sl.sumqty, 0) AS bigint) + CAST(ISNULL(al.sumqty, 0) AS bigint)) AS 'InSale',
                                ISNULL(pl.sumqty, 0) As 'InBuy',
                                (CAST(ISNULL(SUM$Quantity, 0) AS bigint)) - ((CAST(ISNULL(sl.sumqty, 0) AS bigint) + CAST(ISNULL(al.sumqty, 0) AS bigint))) AS 'Lack'
                                FROM [BC18_InkPro].[dbo].[Inkpro$Item$437dbf0e-84ff-417a-965d-ed2bb9650972] item
                                    LEFT JOIN [BC18_InkPro].[dbo].[Inkpro$Item Ledger Entry$437dbf0e-84ff-417a-965d-ed2bb9650972$VSIFT$Key2] qty ON item.No_ = qty.[Item No_]
                                    LEFT JOIN (SELECT No_, SUM([Outstanding Quantity]) As sumqty FROM [BC18_InkPro].[dbo].[Inkpro$Sales Line$437dbf0e-84ff-417a-965d-ed2bb9650972]
                                        WHERE [Document Type] = 1
                                        GROUP BY No_) sl ON item.No_ = sl.No_
                                    LEFT JOIN (SELECT No_, SUM([Remaining Quantity]) As sumqty FROM [BC18_InkPro].[dbo].[Inkpro$Assembly Line$437dbf0e-84ff-417a-965d-ed2bb9650972]
                                        GROUP BY No_) al ON item.No_ = al.No_
                                    LEFT JOIN (SELECT No_, CAST(SUM([Quantity] - [Qty_ to Receive] - [Quantity Received]) AS bigint) As sumqty
                                    FROM [BC18_InkPro].[dbo].[Inkpro$Purchase Line$437dbf0e-84ff-417a-965d-ed2bb9650972] WHERE [Document Type] = 1
                                        GROUP BY No_) pl ON item.No_ = pl.No_
                                WHERE item.[Description] NOT LIKE 'Rabat!%' AND item.No_ NOT IN ('146752', 'DIVERSE', '146441') ORDER BY Lack"

        If Not objSQL01Conn.State = ConnectionState.Open Then
            objSQL01Conn.Open()
        End If

        Dim SqlRdr As SqlDataReader = SqlCmd.ExecuteReader()

        Dim arrProducts As New List(Of String())
        While SqlRdr.Read()

            arrProducts.Add({SqlRdr("ProductId"), SqlRdr("Title"), SqlRdr("InStock"), SqlRdr("InSale"), SqlRdr("InBuy"), SqlRdr("Lack")})

        End While
        SqlRdr.Close()
        SqlRdr.Dispose()

        If Not objSQL01Conn.State = ConnectionState.Closed Then
            objSQL01Conn.Close()
        End If

        lblSearchResults.Text = "<table id=""tblproducts"" class=""table table-striped table-bordered"" style=""width:100%"">
            <thead>
                <tr>
                    <th nowrap>Id</th>
                    <th nowrap>Titel</th>
                    <th nowrap>På lager</th>
                    <th nowrap>I salg</th>
                    <th nowrap>I køb</th>
                    <th nowrap>I rest</th>
                </tr>
            </thead>
            <tbody>"

        For Each product In arrProducts

            lblSearchResults.Text = lblSearchResults.Text & "<tr><td align=""right"" nowrap>" & product(0) & "</td><td>" & product(1) &
                "</td><td align=""right"" nowrap>" & FormatNumber(product(2), 0) & "</td><td align=""right"" nowrap>" & FormatNumber(product(3), 0) &
                "</td><td align=""right"" nowrap>" & FormatNumber(product(4), 0) & "</td><td align=""right"" nowrap>" & FormatNumber(product(5), 0) &
                "</td></tr>"
        Next

        lblSearchResults.Text = lblSearchResults.Text & "</tbody></table>"

        arrProducts.Clear()

    End Sub

End Class