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
    Public strLineChartOrdersForTodayCreatedToday As String = ""
    Public strLineChartOrdersForTodayReadyToPickCreatedToday As String = ""
    Public strLineChartOrdersForTodayShippedToday As String = ""
    Public strLineChartAverageCreatedToday As String = ""
    Public strLineChartAverageShippedToday As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            GetData()
        End If

    End Sub

    'Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick

    '    GetData()

    'End Sub

    Protected Sub GetData()

        Dim SqlCmdGetData As SqlCommand = objWEB01Conn.CreateCommand
        SqlCmdGetData.CommandText = "SELECT TOP 1 * FROM [insight].[dbo].[wh_snapshots] ORDER BY Id DESC"

        Try

            If Not objWEB01Conn.State = ConnectionState.Open Then
                objWEB01Conn.Open()
            End If

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

            Dim SqlReader As SqlDataReader = SqlCmdGetData.ExecuteReader()

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

            strPieChartData = intPickZone_Kompatibel & ", " & intPickZone_Toner & ", " & intPickZone_Original &
                              ", " & intPickZone_Office & ", " & intPickZone_Bestilling

            intShippedYesterdaySameTime = GetShippedYesterdaySameTime()

            GetAverages()
            GetDataForchartOrdersForToday()

        Catch ex As Exception
            Response.Write(ex.ToString())
        Finally
            If Not objWEB01Conn.State = ConnectionState.Closed Then
                objWEB01Conn.Close()
            End If
        End Try

    End Sub

    Protected Function GetShippedYesterdaySameTime()

        Dim intGetYesterDayOrdersTotal As Integer = 0

        Try

            Dim SqlCmdGetYesterDayOrdersTotal As SqlCommand = objWEB01Conn.CreateCommand
            SqlCmdGetYesterDayOrdersTotal.CommandText = "SELECT TOP 1 (ShippedToday_DK + ShippedToday_SE + ShippedToday_NO) As YesterdayOrdersTotal FROM [insight].[dbo].[wh_snapshots]
                                    WHERE SnapshotCreatedAt <= DATEADD(DAY, -1, GETDATE())
                                    ORDER BY SnapshotCreatedAt DESC"

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

    Protected Sub GetDataForchartOrdersForToday()

        Dim arrSetColumns As List(Of String()) = New List(Of String())
        arrSetColumns.Add({"CreatedToday_DK + CreatedToday_SE + CreatedToday_NO", "CreatedToday"})
        arrSetColumns.Add({"ReadyToPickCreatedToday", "ReadyToPickCreatedToday"})
        arrSetColumns.Add({"ShippedToday_DK + ShippedToday_SE + ShippedToday_NO", "ShippedToday"})

        Dim dtGetCurrentTime As Date = Now()

        Dim arrSetIntervals As List(Of String) = New List(Of String)

        'MsgBox("time: " & dtGetCurrentTime.ToString("HH") & "minut: " & dtGetCurrentTime.ToString("mm"))

        If dtGetCurrentTime.ToString("HH:mm") >= "00:00" Then
            arrSetIntervals.Add("00:59:59.999")
        End If
        If dtGetCurrentTime.ToString("HH:mm") >= "08:00" Then
            arrSetIntervals.Add("07:59:59.999")
        End If
        If dtGetCurrentTime.ToString("HH:mm") >= "08:30" Then
            arrSetIntervals.Add("08:29:59.999")
        End If
        If dtGetCurrentTime.ToString("HH:mm") >= "09:00" Then
            arrSetIntervals.Add("08:59:59.999")
        End If
        If dtGetCurrentTime.ToString("HH:mm") >= "09:30" Then
            arrSetIntervals.Add("09:29:59.999")
        End If
        If dtGetCurrentTime.ToString("HH:mm") >= "10:00" Then
            arrSetIntervals.Add("09:59:59.999")
        End If
        If dtGetCurrentTime.ToString("HH:mm") >= "10:30" Then
            arrSetIntervals.Add("10:29:59.999")
        End If
        If dtGetCurrentTime.ToString("HH:mm") >= "11:00" Then
            arrSetIntervals.Add("10:59:59.999")
        End If
        If dtGetCurrentTime.ToString("HH:mm") >= "11:30" Then
            arrSetIntervals.Add("11:29:59.999")
        End If
        If dtGetCurrentTime.ToString("HH:mm") >= "12:00" Then
            arrSetIntervals.Add("11:59:59.999")
        End If
        If dtGetCurrentTime.ToString("HH:mm") >= "12:30" Then
            arrSetIntervals.Add("12:29:59.999")
        End If
        If dtGetCurrentTime.ToString("HH:mm") >= "13:00" Then
            arrSetIntervals.Add("12:59:59.999")
        End If
        If dtGetCurrentTime.ToString("HH:mm") >= "13:30" Then
            arrSetIntervals.Add("13:29:59.999")
        End If
        If dtGetCurrentTime.ToString("HH:mm") >= "14:00" Then
            arrSetIntervals.Add("13:59:59.999")
        End If
        If dtGetCurrentTime.ToString("HH:mm") >= "14:30" Then
            arrSetIntervals.Add("14:29:59.999")
        End If
        If dtGetCurrentTime.ToString("HH:mm") >= "15:00" Then
            arrSetIntervals.Add("14:59:59.999")
        End If
        If dtGetCurrentTime.ToString("HH:mm") >= "15:30" Then
            arrSetIntervals.Add("15:29:59.999")
        End If
        If dtGetCurrentTime.ToString("HH:mm") >= "16:00" Then
            arrSetIntervals.Add("15:59:59.999")
        End If
        If dtGetCurrentTime.ToString("HH:mm") >= "16:30" Then
            arrSetIntervals.Add("16:29:59.999")
        End If
        If dtGetCurrentTime.ToString("HH:mm") >= "17:00" Then
            arrSetIntervals.Add("16:59:59.999")
        End If
        If dtGetCurrentTime.ToString("HH:mm") >= "17:30" Then
            arrSetIntervals.Add("17:29:59.999")
        End If
        If dtGetCurrentTime.ToString("HH") >= 18 Then
            arrSetIntervals.Add("23:59:59.999")
        End If

        Dim arrLineChartReadyToPickCreatedToday As List(Of Integer) = New List(Of Integer)
        Dim arrLineChartDataCreatedToday As List(Of Integer) = New List(Of Integer)
        Dim arrLineChartDataShippedToday As List(Of Integer) = New List(Of Integer)

        For Each column In arrSetColumns

            For Each interval In arrSetIntervals

                If column(1) = "CreatedToday" Then
                    arrLineChartDataCreatedToday.Add(FunctionGetDataForchartOrdersForToday(column(0), interval))
                ElseIf column(1) = "ReadyToPickCreatedToday" Then
                    arrLineChartReadyToPickCreatedToday.Add(FunctionGetDataForchartOrdersForToday(column(0), interval))
                ElseIf column(1) = "ShippedToday" Then
                    arrLineChartDataShippedToday.Add(FunctionGetDataForchartOrdersForToday(column(0), interval))
                End If

            Next

        Next

        strLineChartOrdersForTodayCreatedToday = ("[" & String.Join(", ", arrLineChartDataCreatedToday.ToArray) & "]")
        strLineChartOrdersForTodayReadyToPickCreatedToday = ("[" & String.Join(", ", arrLineChartReadyToPickCreatedToday.ToArray) & "]")
        strLineChartOrdersForTodayShippedToday = ("[" & String.Join(", ", arrLineChartDataShippedToday.ToArray) & "]")

    End Sub

    Protected Function FunctionGetDataForchartOrdersForToday(ByVal strColumns As String, ByVal dtSnapshotCreatedAt As String)

        Dim intValueForLineChart As Integer = 0

        Try

            Dim SqlCmdGetYesterDayOrdersTotal As SqlCommand = objWEB01Conn.CreateCommand
            SqlCmdGetYesterDayOrdersTotal.CommandText = "SELECT TOP 1 (" & strColumns & ") FROM insight.dbo.wh_snapshots WHERE SnapshotCreatedAt
                                                        BETWEEN CONCAT(CAST(GETDATE() As DATE), ' 00:00:00.000') AND CONCAT(CAST(GETDATE() As DATE), ' ', @SnapshotCreatedAt) ORDER BY Id DESC"
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

    Protected Sub GetAverages()

        ' Intervals start

        Dim arrSetColumns As List(Of String()) = New List(Of String())
        arrSetColumns.Add({"CreatedToday_DK + CreatedToday_SE + CreatedToday_NO", "CreatedToday"})
        arrSetColumns.Add({"ShippedToday_DK + ShippedToday_SE + ShippedToday_NO", "ShippedToday"})

        Dim dtGetCurrentHour As Date = Now()

        Dim arrSetIntervals As List(Of String) = New List(Of String)

        If dtGetCurrentHour.ToString("HH") > 0 Then
            arrSetIntervals.Add("00:59:59.999")
        End If
        If dtGetCurrentHour.ToString("HH") >= 8 Then
            arrSetIntervals.Add("07:59:59.999")
        End If
        If dtGetCurrentHour.ToString("HH") >= 9 Then
            arrSetIntervals.Add("08:59:59.999")
        End If
        If dtGetCurrentHour.ToString("HH") >= 10 Then
            arrSetIntervals.Add("09:59:59.999")
        End If
        If dtGetCurrentHour.ToString("HH") >= 11 Then
            arrSetIntervals.Add("10:59:59.999")
        End If
        If dtGetCurrentHour.ToString("HH") >= 12 Then
            arrSetIntervals.Add("11:59:59.999")
        End If
        If dtGetCurrentHour.ToString("HH") >= 13 Then
            arrSetIntervals.Add("12:59:59.999")
        End If
        If dtGetCurrentHour.ToString("HH") >= 14 Then
            arrSetIntervals.Add("13:59:59.999")
        End If
        If dtGetCurrentHour.ToString("HH") >= 15 Then
            arrSetIntervals.Add("14:59:59.999")
        End If
        If dtGetCurrentHour.ToString("HH") >= 16 Then
            arrSetIntervals.Add("15:59:59.999")
        End If
        If dtGetCurrentHour.ToString("HH") >= 17 Then
            arrSetIntervals.Add("16:59:59.999")
        End If
        If dtGetCurrentHour.ToString("HH") >= 18 Then
            arrSetIntervals.Add("17:59:59.999")
        End If

        Dim arrLineChartDataAll As List(Of Integer) = New List(Of Integer)
        Dim arrLineChartDataCreatedToday As List(Of Integer) = New List(Of Integer)
        Dim arrLineChartDataShippedToday As List(Of Integer) = New List(Of Integer)

        For Each column In arrSetColumns

            For Each interval In arrSetIntervals

                If column(1) = "CreatedToday" Then
                    arrLineChartDataCreatedToday.Add(FunctionGetAverages(column(0), interval))
                ElseIf column(1) = "ShippedToday" Then
                    arrLineChartDataShippedToday.Add(FunctionGetAverages(column(0), interval))
                End If

            Next

        Next

        strLineChartAverageCreatedToday = ("[" & String.Join(", ", arrLineChartDataCreatedToday.ToArray) & "]")
        strLineChartAverageShippedToday = ("[" & String.Join(", ", arrLineChartDataShippedToday.ToArray) & "]")

    End Sub

    Protected Function FunctionGetAverages(ByVal strColumns As String, ByVal dtSnapshotCreatedAt As String)

        ' Intervals end

        Dim intGetAverageNow As Integer = 0
        Dim intGetAverage1HourAgo As Integer = 0
        Dim intReturnValue As Integer = 0

        Dim dtGetCurrentTime As Date = Now()

        Dim strGetMinute As String = dtGetCurrentTime.ToString("mm")

        Try

            If Not objWEB01Conn.State = ConnectionState.Open Then
                objWEB01Conn.Open()
            End If

            Dim SqlCmdGetAverageNow As SqlCommand = objWEB01Conn.CreateCommand
            SqlCmdGetAverageNow.CommandText = "SELECT TOP 1 (" & strColumns & ") FROM insight.dbo.wh_snapshots WHERE SnapshotCreatedAt
                                                        BETWEEN CONCAT(CAST(GETDATE() As DATE), ' 00:00:00.000') AND CONCAT(CAST(GETDATE() As DATE), ' ', @SnapshotCreatedAt) ORDER BY Id DESC"
            SqlCmdGetAverageNow.Parameters.AddWithValue("@SnapshotCreatedAt", dtSnapshotCreatedAt)
            intGetAverageNow = SqlCmdGetAverageNow.ExecuteScalar()
            SqlCmdGetAverageNow.Parameters.Clear()

            Dim SqlCmdGetAverage1HourAgo As SqlCommand = objWEB01Conn.CreateCommand
            SqlCmdGetAverage1HourAgo.CommandText = "SELECT TOP 1 (" & strColumns & ") FROM insight.dbo.wh_snapshots WHERE SnapshotCreatedAt
                                                        BETWEEN DATEADD(HOUR, -1, CONCAT(CAST(GETDATE() As DATE), ' 01:00:00.000')) AND DATEADD(HOUR, -1, CONCAT(CAST(GETDATE() As DATE), ' ', @SnapshotCreatedAt)) ORDER BY Id DESC"
            SqlCmdGetAverage1HourAgo.Parameters.AddWithValue("@SnapshotCreatedAt", dtSnapshotCreatedAt)
            intGetAverage1HourAgo = SqlCmdGetAverage1HourAgo.ExecuteScalar()
            SqlCmdGetAverage1HourAgo.Parameters.Clear()

            intReturnValue = intGetAverageNow - intGetAverage1HourAgo



            If Not objWEB01Conn.State = ConnectionState.Closed Then
                objWEB01Conn.Close()
            End If


            'intGetAverage = (intGetAverage / 60) * strGetMinute



        Catch ex As Exception
            Response.Write(ex.ToString())
        Finally
            If Not objWEB01Conn.State = ConnectionState.Closed Then
                objWEB01Conn.Close()
            End If
        End Try

        Return intReturnValue

    End Function

End Class