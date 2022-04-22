<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="default.aspx.vb" Inherits="insight._WarehouseDashboard" %>

<!--
=========================================================
* Paper Dashboard 2 - v2.0.1
=========================================================

* Product Page: https://www.creative-tim.com/product/paper-dashboard-2
* Copyright 2020 Creative Tim (https://www.creative-tim.com)

Coded by www.creative-tim.com

 =========================================================

* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
-->
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
</head>

<body class="">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                <asp:Timer ID="Timer1" runat="server" Interval="30000" ontick="Timer1_Tick"></asp:Timer>

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
                      <p class="card-category">Ordrer i alt</p>
                    <asp:UpdatePanel ID="uPnlOrders_All" runat="server">
                        <ContentTemplate>
                            <p class="card-title"><%=FormatNumber(intAll_Total, 0)%><p>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>
                    </div>
                  </div>
                </div>
              </div>
              <div class="card-footer " align="right">
                <hr>
                    <asp:UpdatePanel ID="uPnlCreatedToday" runat="server">
                        <ContentTemplate>
                            <div class="stats"><%=FormatNumber(intCreatedToday_Total, 0)%> nye salgsordrer i dag</div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>
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
                    <asp:UpdatePanel ID="uPnlReadyToPickAll" runat="server">
                        <ContentTemplate>
                        <p class="card-title"><%=FormatNumber(intReadyToPickAll, 0)%><p>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>
                    </div>
                  </div>
                </div>
              </div>
              <div class="card-footer " align="right">
                <hr>
                    <asp:UpdatePanel ID="uPnlReadyToPickCreatedToday" runat="server">
                        <ContentTemplate>
                            <div class="stats"><%=FormatNumber(intReadyToPickCreatedToday, 0)%> ordrer frigivet til pluk i dag</div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>
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
                    <asp:UpdatePanel ID="uPnlPickedReadyToShip" runat="server">
                        <ContentTemplate>
                        <p class="card-title"><%=FormatNumber(intPickedReadyToShip, 0)%><p>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>
                    </div>
                  </div>
                </div>
              </div>
              <div class="card-footer " align="right">
                <hr>
                    <asp:UpdatePanel ID="uPnlSplit" runat="server">
                        <ContentTemplate>
                            <div class="stats">
                                <%=FormatNumber(intSplitAll, 0)%> delordrer i alt | 
                                <%=FormatNumber(intSplitSentToday, 0)%> oprettet i dag
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>
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
                    <asp:UpdatePanel ID="uPnlShippedToday_Total" runat="server">
                        <ContentTemplate>
                        <p class="card-title"><%=FormatNumber(intShippedToday_Total, 0)%><p>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>
                    </div>
                  </div>
                </div>
              </div>
              <div class="card-footer " align="right">
                <hr>
                    <asp:UpdatePanel ID="uPnlShippedYesterdaySameTime" runat="server">
                        <ContentTemplate>
                            <div class="stats">
                                <%=FormatNumber(intShippedYesterdaySameTime, 0)%> sendt samme tid i går
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>
              </div>
            </div>
          </div>
        </div>
       <div class="row">
          <div class="col-md-4">
            <div class="card ">
              <div class="card-header ">
                <h5 class="card-title">Ordrer til pluk pr. zone</h5>
                <%--<p class="card-category">Last Campaign Performance</p>--%>
              </div>
              <div class="card-body ">
                <canvas id="chartOrdersByPickZone"></canvas>
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
            <div class="card card-chart">
              <div class="card-header">
                <h5 class="card-title">Backlog vs. dagens ordrer</h5>
                <%--<p class="card-category">Line Chart with Points</p>--%>
              </div>
              <div class="card-body">
                <canvas id="speedChart" width="400" height="94"></canvas>
              </div>
              <div class="card-footer">
                <div class="chart-legend">
                  <i class="fa fa-circle text-danger"></i> Alle ordrer
                  <i class="fa fa-circle text-primary" style="margin-left: 5%;"></i> Nye ordrer
                  <i class="fa fa-circle text-success" style="margin-left: 5%;"></i> Afsendt
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
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

    <asp:UpdatePanel ID="uPnlPieChart" runat="server">
        <ContentTemplate>

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
          data: <%=strPieChartData%>
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

        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
            </Triggers>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="uPnlLineChart" runat="server">
        <ContentTemplate>

    <script>
            var speedCanvas = document.getElementById("speedChart");

    var dataFirst = {
      data: <%=strLineChartDataAll%>,
      fill: false,
      borderColor: '#df4759',
      backgroundColor: 'transparent',
      pointBorderColor: '#df4759',
      pointRadius: 0,
      pointHoverRadius: 4,
      pointBorderWidth: 8,
    };

    var dataSecond = {
      data: <%=strLineChartDataCreatedToday%>,
      fill: false,
      borderColor: '#7c69ef',
      backgroundColor: 'transparent',
      pointBorderColor: '#7c69ef',
      pointRadius: 0,
      pointHoverRadius: 4,
      pointBorderWidth: 8
    };

    var dataThird = {
      data: <%=strLineChartDataShippedToday%>,
      fill: false,
      borderColor: '#42ba96',
      backgroundColor: 'transparent',
      pointBorderColor: '#42ba96',
      pointRadius: 0,
      pointHoverRadius: 4,
      pointBorderWidth: 8
    };

    var speedData = {
      labels: ["00-08", "08-09", "09-10", "10-11", "11-12", "12-13", "13-14", "14-15", "15-16", "16-17", "17-18", "18-00"],
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

        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
            </Triggers>
    </asp:UpdatePanel>


  <!--  Notifications Plugin    -->
  <script src="assets/js/plugins/bootstrap-notify.js"></script>
  <!-- Control Center for Now Ui Dashboard: parallax effects, scripts for the example pages etc -->
  <script src="assets/js/paper-dashboard.min.js?v=2.0.1" type="text/javascript"></script>



</form>
</body>

</html>

