Imports System.Drawing
Imports System.Web.UI.DataVisualization.Charting

Public Class convertjavatocodebehind
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Dim dt As New DataTable()
        dt.Columns.Add("ColorName")
        dt.Columns.Add("Percentage")
        Dim dr1 As DataRow = dt.NewRow()
        dr1("ColorName") = "red"
        dr1("Percentage") = "30"
        dt.Rows.Add(dr1)

        Dim dr2 As DataRow = dt.NewRow()
        dr2("ColorName") = "green"
        dr2("Percentage") = "50"
        dt.Rows.Add(dr2)
        Dim dr3 As DataRow = dt.NewRow()
        dr3("ColorName") = "yellow"
        dr3("Percentage") = "20"
        dt.Rows.Add(dr3)
        Dim chartData As New Dictionary(Of String, Integer)()
        For Each r As DataRow In dt.Rows
            Dim key As String = r("ColorName").ToString()
            Dim value As Integer = Convert.ToInt32(r("Percentage"))
            chartData.Add(key, value)
        Next r
        Chart1.Series("Series1").Points.DataBind(chartData, "Key", "Value", String.Empty)
        Chart1.Series("Series1").ChartType = SeriesChartType.Pie
        Chart1.ChartAreas(0).Area3DStyle.Enable3D = True
        Chart1.ChartAreas(0).Area3DStyle.Inclination = Convert.ToInt32(20)
        For Each charts As Series In Chart1.Series
            For Each point As DataPoint In charts.Points
                Select Case point.AxisLabel
                    Case "red"
                        point.Color = Color.Red
                    Case "green"
                        point.Color = Color.Green
                    Case "yellow"
                        point.Color = Color.Yellow
                End Select
            Next point
        Next charts

    End Sub

    'Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick



    '    '        strdfdf = "<script>
    '    'setInterval(Function() {
    '    '      updatedDataSet = [" & inthundreds & "];
    '    '    updateBarGraph(barChart,'Prediction', colouarray, updatedDataSet);
    '    '  }, 1000);
    '    '</script>"

    '    'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), strdfdf, Me.ID, False)
    '    'ScriptManager.RegisterStartupScript(Page, Page.GetType, "ShowInfoPage", strdfdf, False)



    'End Sub

    Protected Sub Chart1_Load(sender As Object, e As EventArgs) Handles Chart1.Load

    End Sub
End Class