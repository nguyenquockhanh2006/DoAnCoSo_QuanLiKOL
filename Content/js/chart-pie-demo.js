//
var th100 = document.getElementById("1+00").value;
var th200 = document.getElementById("2+00").value;
var th300 = document.getElementById("3+00").value;
var th400 = document.getElementById("4+00").value;
var th500 = document.getElementById("5+00").value;
var th600 = document.getElementById("6+00").value;
// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#292b2c';

// Pie Chart Example
var ctx = document.getElementById("myPieChart");
var myPieChart = new Chart(ctx, {
    type: 'pie',
    data: {
        labels: ["Music", "Culinary", "Cosmetics", "Ecommerce", "Sport", "Fashion"],
        datasets: [{
            data: [th100, th200, th300, th400, th500, th600],
            backgroundColor: ['#007bff', '#dc3545', '#ffc107', '#28a745', '#ff66ff','cccccc'],
        }],
    },
});
