<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="default.aspx.vb" Inherits="insight._WarehouseDashboard" %>

<!DOCTYPE html>
<html lang="en">

<head>
  <meta charset="utf-8" />
  <link rel="apple-touch-icon" sizes="76x76" href="assets/img/apple-icon.png">
  <link rel="icon" type="image/png" href="assets/img/favicon.png">
  <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
  <title>
    INKPRO A/S Warehouse Insight
  </title>
  <meta content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0, shrink-to-fit=no' name='viewport' />
  <!--     Fonts and icons     -->
  <link href="https://fonts.googleapis.com/css?family=Montserrat:400,700,200" rel="stylesheet" />
  <link href="https://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css" rel="stylesheet">
  <!-- CSS Files -->
  <link href="assets/css/bootstrap.min.css" rel="stylesheet" />
  <link href="assets/css/paper-dashboard.css?v=2.0.1" rel="stylesheet" />
  <meta http-equiv="refresh" content="30">
</head>

<body class="">
    <form id="form1" runat="server">
<%--        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                <asp:Timer ID="Timer1" runat="server" Interval="30000" ontick="Timer1_Tick"></asp:Timer>--%>

  <div class="wrapper ">
    <div class="main-panel">
      <!-- Navbar -->
      
      <!-- End Navbar -->
      <div class="content">
        <div class="row">
          <div class="col-lg-3 col-md-6 col-sm-6">
            <div class="card card-stats">
              <div class="card-body ">
                <div class="row">
                  <div class="col-5 col-md-4">
                    <div class="icon-big text-center icon-warning">
                      <i class="nc-icon nc-basket text-danger"></i>
                    </div>
                  </div>
                  <div class="col-7 col-md-8">
                    <div class="numbers">
                      <p class="card-category">Ordrer i dag</p>
<%--                    <asp:UpdatePanel ID="uPnlOrders_All" runat="server">
                        <ContentTemplate>--%>
                            <p class="card-title"><%=FormatNumber(intCreatedToday_Total, 0)%><p>
<%--                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
                    </div>
                  </div>
                </div>
              </div>
              <div class="card-footer " align="right">
                <hr>
<%--                    <asp:UpdatePanel ID="uPnlCreatedToday" runat="server">
                        <ContentTemplate>--%>
                            <div class="stats"><%=FormatNumber(intAll_Total, 0)%> ordrer i alt | <%=FormatNumber((intAll_Total - intReadyToPickAll), 0)%> ikke frigivet</div>
<%--                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
              </div>
            </div>
          </div>
          <div class="col-lg-3 col-md-6 col-sm-6">
            <div class="card card-stats">
              <div class="card-body ">
                <div class="row">
                  <div class="col-5 col-md-4">
                    <div class="icon-big text-center icon-warning">
                      <i class="nc-icon nc-cart-simple text-warning"></i>
                    </div>
                  </div>
                  <div class="col-7 col-md-8">
                    <div class="numbers">
                      <p class="card-category">Ordrer til pluk</p>
<%--                    <asp:UpdatePanel ID="uPnlReadyToPickAll" runat="server">
                        <ContentTemplate>--%>
                        <p class="card-title"><%=FormatNumber(intReadyToPickAll, 0)%><p>
<%--                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
                    </div>
                  </div>
                </div>
              </div>
              <div class="card-footer " align="right">
                <hr>
<%--                    <asp:UpdatePanel ID="uPnlReadyToPickCreatedToday" runat="server">
                        <ContentTemplate>--%>
                            <div class="stats"><%=FormatNumber(intReadyToPickCreatedToday, 0)%> frigivet til pluk i dag</div>
<%--                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
              </div>
            </div>
          </div>
          <div class="col-lg-3 col-md-6 col-sm-6">
            <div class="card card-stats">
              <div class="card-body ">
                <div class="row">
                  <div class="col-5 col-md-4">
                    <div class="icon-big text-center icon-warning">
                      <i class="nc-icon nc-email-85 text-primary"></i>
                    </div>
                  </div>
                  <div class="col-7 col-md-8">
                    <div class="numbers">
                      <p class="card-category">Plukket, klar til pak</p>
<%--                    <asp:UpdatePanel ID="uPnlPickedReadyToShip" runat="server">
                        <ContentTemplate>--%>
                        <p class="card-title"><%=FormatNumber(intPickedReadyToShip, 0)%><p>
<%--                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
                    </div>
                  </div>
                </div>
              </div>
              <div class="card-footer " align="right">
                <hr>
<%--                    <asp:UpdatePanel ID="uPnlSplit" runat="server">
                        <ContentTemplate>--%>
                            <div class="stats">
                                <%=FormatNumber(intSplitAll, 0)%> delordrer i alt | 
                                <%=FormatNumber(intSplitSentToday, 0)%> oprettet i dag
                            </div>
<%--                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
              </div>
            </div>
          </div>
          <div class="col-lg-3 col-md-6 col-sm-6">
            <div class="card card-stats">
              <div class="card-body ">
                <div class="row">
                  <div class="col-5 col-md-4">
                    <div class="icon-big text-center icon-warning">
                      <i class="nc-icon nc-delivery-fast text-success"></i>
                    </div>
                  </div>
                  <div class="col-7 col-md-8">
                    <div class="numbers">
                      <p class="card-category">Afsendt</p>
<%--                    <asp:UpdatePanel ID="uPnlShippedToday_Total" runat="server">
                        <ContentTemplate>--%>
                        <p class="card-title"><%=FormatNumber(intShippedToday_Total, 0)%><p>
<%--                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
                    </div>
                  </div>
                </div>
              </div>
              <div class="card-footer " align="right">
                <hr>
<%--                    <asp:UpdatePanel ID="uPnlShippedYesterdaySameTime" runat="server">
                        <ContentTemplate>--%>
                            <div class="stats">
                                <%=FormatNumber(intShippedYesterdaySameTime, 0)%> samme tid i går
                            </div>
<%--                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
              </div>
            </div>
          </div>
        </div>
       <div class="row">
          <div class="col-md-4">
            <div class="card ">
              <div class="card-header ">
                <h5 class="card-title" style="margin-bottom:0px;" align="center">Pluk fordelt på zoner</h5>
                <%--<p class="card-category">Last Campaign Performance</p>--%>
              </div>
              <div class="card-body ">
                <canvas id="chartOrdersByPickZone" height="130"></canvas>
              </div>
              <div class="card-footer ">
                <div class="legend" align="center">
                  <i class="fa fa-circle text-danger"></i> Kompatibel
                  <i class="fa fa-circle text-success" style="margin-left: 5%;"></i> Toner
                  <i class="fa fa-circle text-warning" style="margin-left: 5%;"></i> Original
                  <i class="fa fa-circle text-primary" style="margin-left: 5%;"></i> Office
                  <i class="fa fa-circle text-gray" style="margin-left: 5%;"></i> Bestilling
                </div>
              </div>
            </div>
          </div>

          <div class="col-md-8">
            <div class="card ">
              <div class="card-header ">
                <h5 class="card-title" style="margin-bottom:0px;" align="center">Gennemsnit pr. time</h5>
                <%--<p class="card-category">Last Campaign Performance</p>--%>
              </div>
              <div class="card-body ">
                <canvas id="chartOrdersAverages" height="62"></canvas>
              </div>
              <div class="card-footer ">
                <div class="legend" align="center">
                  <i class="fa fa-circle text-danger"></i> Nye
                  <i class="fa fa-circle text-success" style="margin-left: 5%;"></i> Afsendt
                </div>
              </div>
            </div>
          </div>          


          
        </div>
        <div class="row">
            <div class="col-md-12">
            <div class="card card-chart" style="margin-bottom:10px;">
              <div class="card-header">
                <h5 class="card-title" align="center">Ordrer for i dag</h5>
                <%--<p class="card-category">Line Chart with Points</p>--%>
              </div>
              <div class="card-body" style="padding-bottom:0px;">
                <canvas id="chartOrdersForToday" height="40"></canvas>
              </div>
              <div class="card-footer" align="center" style="margin-top:0px;">
                <div class="chart-legend">
                  <i class="fa fa-circle text-danger"></i> Nye
                  <i class="fa fa-circle text-warning" style="margin-left: 5%;"></i> Til pluk
                  <i class="fa fa-circle text-success" style="margin-left: 5%;"></i> Afsendt
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

    </div>

  </div>
                              <div id="watermarkbg">
            <p id="bg-text">Data opdateret <%=strSnapshotCreatedAt %></p>
        </div>


  <!--   Core JS Files   -->
  <script src="assets/js/core/jquery.min.js"></script>
  <script src="assets/js/core/popper.min.js"></script>
  <script src="assets/js/core/bootstrap.min.js"></script>
  <script src="assets/js/plugins/perfect-scrollbar.jquery.min.js"></script>
  <!--  Google Maps Plugin    -->
  <script src="https://maps.googleapis.com/maps/api/js?key=YOUR_KEY_HERE"></script>
  <!-- Chart JS -->
  <script src="assets/js/plugins/chartjs.min.js"></script>

    <script>
          ctx = document.getElementById('chartOrdersByPickZone').getContext("2d");

    myChart = new Chart(ctx, {
      type: 'pie',
      data: {
        labels: [1, 2, 3, 4, 5],
        datasets: [{
          label: "PickZones",
          pointRadius: 0,
          pointHoverRadius: 0,
          backgroundColor: [
            '#df4759',
            '#42ba96',
            '#ffc107',
            '#7c69ef',
            '#ccc',
          ],
          borderWidth: 0,
          data: [<%=strPieChartData%>]
        }]
      },

      options: {

        animation: {
            duration: 0,
    },

        legend: {
          display: false
        },

        pieceLabel: {
          render: 'percentage',
          fontColor: ['white'],
          precision: 2
        },

        tooltips: {
          enabled: false
        },

        scales: {
          yAxes: [{

            ticks: {
              display: false
            },
            gridLines: {
              drawBorder: false,
              zeroLineColor: "transparent",
              color: 'rgba(255,255,255,0.05)'
            }

          }],

          xAxes: [{
            barPercentage: 1.6,
            gridLines: {
              drawBorder: false,
              color: 'rgba(255,255,255,0.1)',
              zeroLineColor: "transparent"
            },
            ticks: {
              display: false,
            }
          }]
        },
      }
    });

  </script>

    <script>
            var speedCanvas = document.getElementById("chartOrdersAverages");

    var dataFirst = {
      data: <%=strLineChartAverageCreatedToday%>,
      fill: false,
      borderColor: '#df4759',
      backgroundColor: 'transparent',
      pointBorderColor: '#df4759',
      pointRadius: 0,
      pointHoverRadius: 4,
      pointBorderWidth: 8,
    };

    var dataSecond = {
      data: <%=strLineChartAverageShippedToday%>,
      fill: false,
      borderColor: '#42ba96',
      backgroundColor: 'transparent',
      pointBorderColor: '#42ba96',
      pointRadius: 0,
      pointHoverRadius: 4,
      pointBorderWidth: 8
    };

    var speedData = {
      labels: ["00:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "00:00"],
      datasets: [dataFirst, dataSecond]
    };

        var chartOptions = {

        animation: {
        duration: 0
    },

      legend: {
        display: false,
        position: 'top'
      }
    };

    var lineChart = new Chart(speedCanvas, {
      type: 'line',
      hover: false,
      data: speedData,
      options: chartOptions
    });
    </script>
        
    <script>
            var speedCanvas = document.getElementById("chartOrdersForToday");

    var dataFirst = {
      data: <%=strLineChartOrdersForTodayCreatedToday%>,
      fill: false,
      borderColor: '#df4759',
      backgroundColor: 'transparent',
      pointBorderColor: '#df4759',
      pointRadius: 0,
      pointHoverRadius: 4,
      pointBorderWidth: 8,
    };

    var dataSecond = {
      data: <%=strLineChartOrdersForTodayReadyToPickCreatedToday%>,
      fill: false,
      borderColor: '#ffc107',
      backgroundColor: 'transparent',
      pointBorderColor: '#ffc107',
      pointRadius: 0,
      pointHoverRadius: 4,
      pointBorderWidth: 8
    };

    var dataThird = {
      data: <%=strLineChartOrdersForTodayShippedToday%>,
      fill: false,
      borderColor: '#42ba96',
      backgroundColor: 'transparent',
      pointBorderColor: '#42ba96',
      pointRadius: 0,
      pointHoverRadius: 4,
      pointBorderWidth: 8
    };

    var speedData = {
      labels: ["00:00", "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "00:00"],
      datasets: [dataFirst, dataSecond, dataThird]
    };

        var chartOptions = {

        animation: {
        duration: 0
    },

      legend: {
        display: false,
        position: 'top'
      }
    };

    var lineChart = new Chart(speedCanvas, {
      type: 'line',
      hover: false,
      data: speedData,
      options: chartOptions
    });
    </script>

  <!--  Notifications Plugin    -->
  <script src="assets/js/plugins/bootstrap-notify.js"></script>
  <!-- Control Center for Now Ui Dashboard: parallax effects, scripts for the example pages etc -->
  <script src="assets/js/paper-dashboard.min.js?v=2.0.1" type="text/javascript"></script>

</form>
</body>

</html>