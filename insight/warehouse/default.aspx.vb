Imports System.Data.SqlClient
Public Class _WarehouseDashboard
    Inherits Page

    Dim objWEB01Conn As New SqlConnection(ConfigurationManager.ConnectionStrings("web01conn").ConnectionString)

    Public intAll_Total As Integer = 0
    Public intCreatedToday_Total As Integer = 0
    Public intReadyToPickAll As Integer = 0
    Public intReadyToPickCreatedToday As Integer = 0
    Public intPickedReadyToShip As Integer = 0
    Public intSplitAll As Integer = 0
    Public intSplitSentToday As Integer = 0
    Public intShippedToday_Total As Integer = 0
    Public intShippedYesterdaySameTime As Integer = 0
    Public strPieChartData As String = ""
    Public strLineChartDataAll As String = ""
    Public strLineChartDataCreatedToday As String = ""
    Public strLineChartDataShippedToday As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            GetData()
        End If

    End Sub

    Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick

        GetData()

    End Sub

    Protected Sub GetData()

        Dim SqlCmdGetData As SqlCommand = objWEB01Conn.CreateCommand
        SqlCmdGetData.CommandText = "SELECT TOP 1 * FROM [insight].[dbo].[wh_snapshots] ORDER BY Id DESC"

        Try

            If Not objWEB01Conn.State = ConnectionState.Open Then
                objWEB01Conn.Open()
            End If

            Dim SqlReader As SqlDataReader = SqlCmdGetData.ExecuteReader()

            Dim intAll_DK As Integer = 0
            Dim intAll_SE As Integer = 0
            Dim intAll_NO As Integer = 0
            Dim intCreatedToday_DK As Integer = 0
            Dim intCreatedToday_SE As Integer = 0
            Dim intCreatedToday_NO As Integer = 0
            Dim intShippedToday_DK As Integer = 0
            Dim intShippedToday_SE As Integer = 0
            Dim intShippedToday_NO As Integer = 0
            Dim intPickZone_Bestilling As Integer = 0
            Dim intPickZone_Kompatibel As Integer = 0
            Dim intPickZone_Office As Integer = 0
            Dim intPickZone_Original As Integer = 0
            Dim intPickZone_Toner As Integer = 0
            Dim dtSnapshotCreatedAs As Date = Now()

            While SqlReader.Read()

                intAll_DK = SqlReader("All_DK")
                intAll_SE = SqlReader("All_SE")
                intAll_NO = SqlReader("All_NO")
                intCreatedToday_DK = SqlReader("CreatedToday_DK")
                intCreatedToday_SE = SqlReader("CreatedToday_SE")
                intCreatedToday_NO = SqlReader("CreatedToday_NO")
                intSplitAll = SqlReader("SplitAll")
                intSplitSentToday = SqlReader("SplitSentToday")
                intReadyToPickAll = SqlReader("ReadyToPickAll")
                intReadyToPickCreatedToday = SqlReader("ReadyToPickCreatedToday")
                intPickedReadyToShip = SqlReader("PickedReadyToShip")
                intShippedToday_DK = SqlReader("ShippedToday_DK")
                intShippedToday_SE = SqlReader("ShippedToday_SE")
                intShippedToday_NO = SqlReader("ShippedToday_NO")
                intPickZone_Bestilling = SqlReader("PickZone_Bestilling")
                intPickZone_Kompatibel = SqlReader("PickZone_Kompatibel")
                intPickZone_Office = SqlReader("PickZone_Office")
                intPickZone_Original = SqlReader("PickZone_Original")
                intPickZone_Toner = SqlReader("PickZone_Toner")
                dtSnapshotCreatedAs = SqlReader("SnapshotCreatedAt")

            End While

            SqlReader.Close()

            intAll_Total = intAll_DK + intAll_SE + intAll_SE
            intCreatedToday_Total = intCreatedToday_DK + intCreatedToday_SE + intCreatedToday_NO
            intShippedToday_Total = intShippedToday_DK + intShippedToday_SE + intShippedToday_NO

            strPieChartData = "[" & intPickZone_Kompatibel & ", " & intPickZone_Toner & ", " & intPickZone_Original &
                              ", " & intPickZone_Office & ", " & intPickZone_Bestilling & "]"

            intShippedYesterdaySameTime = GetShippedYesterdaySameTime(dtSnapshotCreatedAs)

            GetDAtaForLineChart()

        Catch ex As Exception
            Response.Write(ex.ToString())
        Finally
            If Not objWEB01Conn.State = ConnectionState.Closed Then
                objWEB01Conn.Close()
            End If
        End Try

    End Sub

    Protected Function GetShippedYesterdaySameTime(ByVal dtSnapshotCreatedAt As Date)

        Dim intGetYesterDayOrdersTotal As Integer = 0

        Try

            Dim SqlCmdGetYesterDayOrdersTotal As SqlCommand = objWEB01Conn.CreateCommand
            SqlCmdGetYesterDayOrdersTotal.CommandText = "SELECT TOP 1 SnapshotCreatedAt As YesterdayOrdersTotal FROM [insight].[dbo].[wh_snapshots]
                                    WHERE SnapshotCreatedAt <= DATEADD(DAY, -1, @SnapshotCreatedAt)
                                    ORDER BY SnapshotCreatedAt DESC"
            SqlCmdGetYesterDayOrdersTotal.Parameters.AddWithValue("@SnapshotCreatedAt", dtSnapshotCreatedAt)

            If Not objWEB01Conn.State = ConnectionState.Open Then
                objWEB01Conn.Open()
            End If

            intGetYesterDayOrdersTotal = SqlCmdGetYesterDayOrdersTotal.ExecuteScalar()

            SqlCmdGetYesterDayOrdersTotal.Parameters.Clear()

        Catch ex As Exception
            Response.Write(ex.ToString())
        Finally
            If Not objWEB01Conn.State = ConnectionState.Closed Then
                objWEB01Conn.Close()
            End If
        End Try

        Return intGetYesterDayOrdersTotal

    End Function

    Protected Sub GetDAtaForLineChart()

        Dim arrSetColumns As List(Of String()) = New List(Of String())
        arrSetColumns.Add({"All_DK + All_SE + All_NO", "All"})
        arrSetColumns.Add({"CreatedToday_DK + CreatedToday_SE + CreatedToday_NO", "CreatedToday"})
        arrSetColumns.Add({"ShippedToday_DK + ShippedToday_SE + ShippedToday_NO", "ShippedToday"})

        Dim arrSetIntervals As List(Of String) = New List(Of String)
        arrSetIntervals.Add("07:59:59.999")
        arrSetIntervals.Add("08:59:59.999")
        arrSetIntervals.Add("09:59:59.999")
        arrSetIntervals.Add("10:59:59.999")
        arrSetIntervals.Add("11:59:59.999")
        arrSetIntervals.Add("12:59:59.999")
        arrSetIntervals.Add("13:59:59.999")
        arrSetIntervals.Add("14:59:59.999")
        arrSetIntervals.Add("15:59:59.999")
        arrSetIntervals.Add("16:59:59.999")
        arrSetIntervals.Add("17:59:59.999")
        arrSetIntervals.Add("23:59:59.999")

        Dim arrLineChartDataAll As List(Of Integer) = New List(Of Integer)
        Dim arrLineChartDataCreatedToday As List(Of Integer) = New List(Of Integer)
        Dim arrLineChartDataShippedToday As List(Of Integer) = New List(Of Integer)

        For Each column In arrSetColumns

            For Each interval In arrSetIntervals

                If column(1) = "All" Then
                    arrLineChartDataAll.Add(FunctionGetDataForLineChart(column(0), interval))
                ElseIf column(1) = "CreatedToday" Then
                    arrLineChartDataCreatedToday.Add(FunctionGetDataForLineChart(column(0), interval))
                ElseIf column(1) = "ShippedToday" Then
                    arrLineChartDataShippedToday.Add(FunctionGetDataForLineChart(column(0), interval))
                End If

            Next

        Next

        strLineChartDataAll = ("[" & String.Join(", ", arrLineChartDataAll.ToArray) & "]")
        strLineChartDataCreatedToday = ("[" & String.Join(", ", arrLineChartDataCreatedToday.ToArray) & "]")
        strLineChartDataShippedToday = ("[" & String.Join(", ", arrLineChartDataShippedToday.ToArray) & "]")

    End Sub

    Protected Function FunctionGetDataForLineChart(ByVal strColumns As String, ByVal dtSnapshotCreatedAt As String)

        Dim intValueForLineChart As Integer = 0

        Try

            Dim SqlCmdGetYesterDayOrdersTotal As SqlCommand = objWEB01Conn.CreateCommand
            SqlCmdGetYesterDayOrdersTotal.CommandText = "SELECT TOP 1 (" & strColumns & ") FROM insight.dbo.wh_snapshots WHERE SnapshotCreatedAt
                                                        BETWEEN CONCAT('2022-04-22', ' 00:00:00.000') AND CONCAT('2022-04-22 ', @SnapshotCreatedAt) ORDER BY Id DESC"
            SqlCmdGetYesterDayOrdersTotal.Parameters.AddWithValue("@SnapshotCreatedAt", dtSnapshotCreatedAt)

            If Not objWEB01Conn.State = ConnectionState.Open Then
                objWEB01Conn.Open()
            End If

            intValueForLineChart = SqlCmdGetYesterDayOrdersTotal.ExecuteScalar()

            SqlCmdGetYesterDayOrdersTotal.Parameters.Clear()

        Catch ex As Exception
            Response.Write(ex.ToString())
        Finally
            If Not objWEB01Conn.State = ConnectionState.Closed Then
                objWEB01Conn.Close()
            End If
        End Try

        Return intValueForLineChart

    End Function

End Class