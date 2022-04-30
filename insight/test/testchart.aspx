<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="testchart.aspx.vb" Inherits="insight.testchart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
                    
</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div>
            <canvas id="barChart" width="400" height="400"></canvas>
        </div>

<script src="https://cdn.jsdelivr.net/npm/chart.js"></script>


                                        <script>
//value for x-axis
var emotions = ["calm", "happy", "angry", "disgust"];
//colours for each bar
var colouarray = ['red', 'green', 'yellow', 'blue'];
//Let's initialData[] be the initial data set
var initialData = [2, 4, 6, 8];
//Let's updatedDataSet[] be the array to hold the upadted data set with every update call
var updatedDataSet;
/*Creating the bar chart*/
var ctx = document.getElementById("barChart");
var barChart = new Chart(ctx, {
    type: 'bar',//   w  w   w  .d    e m o    2s    .  c o  m 
    data: {
        labels: emotions,
        datasets: [{
            backgroundColor: colouarray,
            label: 'Prediction',
            data: initialData
        }]
    },
    options: {
        scales: {
            yAxes: [{
                ticks: {
                    beginAtZero: true,
                    min: 2,
                    max: 100,
                    stepSize: 2,
                }
            }]
        }
    }
});
/*Function to update the bar chart*/
function updateBarGraph(chart, label, color, data) {
    chart.data.datasets.pop();
    chart.data.datasets.push({
        label: label,
        backgroundColor: color,
        data: data
    });
    chart.update();
}
setInterval(function () {
      updatedDataSet = [2, 4, 6, 8];
    updateBarGraph(barChart,'Prediction', colouarray, updatedDataSet);
  }, 4000);

                </script>

        <asp:UpdatePanel ID="up1" runat="server">

            <ContentTemplate>

                <script>
                    /*Updating the bar chart with updated data in every second. */

                </script>

                <%=Now()%>



            </ContentTemplate>

            <Triggers>

                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />

            </Triggers>

        </asp:UpdatePanel>



        <asp:Timer ID="Timer1" runat="server" Interval="5000" OnTick="Timer1_Tick"></asp:Timer>

        


    </form>
</body>
</html>
