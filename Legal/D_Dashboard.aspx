<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="D_Dashboard.aspx.cs" Inherits="Legal_D_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .highcharts-figure,
        .highcharts-data-table table {
            min-width: auto;
            max-width: auto;
            margin: 1em auto;
        }

        .highcharts-data-table table {
            font-family: Verdana, sans-serif;
            border-collapse: collapse;
            border: 1px solid #ebebeb;
            margin: 10px auto;
            text-align: center;
            width: 100%;
            max-width: 500px;
        }

        .highcharts-data-table caption {
            padding: 1em 0;
            font-size: 1.2em;
            color: #555;
        }

        .highcharts-data-table th {
            font-weight: 600;
            padding: 0.5em;
        }

        .highcharts-data-table td,
        .highcharts-data-table th,
        .highcharts-data-table caption {
            padding: 0.5em;
        }

        .highcharts-data-table thead tr,
        .highcharts-data-table tr:nth-child(even) {
            background: #f8f8f8;
        }

        .highcharts-data-table tr:hover {
            background: #f1f7ff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <fieldset>
                    <legend>D-Dashboard</legend>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Court</label>
                                <asp:DropDownList ID="ddlCourt" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm">
                            <div class="form-group">
                                <label>Case type </label>
                                <asp:DropDownList ID="ddlCaseType" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm">
                            <div class="form-group">
                                <label>Case Status </label>
                                <asp:DropDownList ID="ddlCaseStatus" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">All</asp:ListItem>
                                    <asp:ListItem Value="1">Pending</asp:ListItem>
                                    <asp:ListItem Value="2">Disposed</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 mt-4">
                            <div class="row">
                                <div class="col-md-6 mt-2">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-block" ValidationGroup="Save" Text="Search" OnClick="btnSearch_Click" />
                                </div>
                                <div class="col-md-6 mt-2">
                                    <a href="D_Dashboard.aspx" class="btn btn-default btn-block">Clear</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </fieldset>


                <div class="row">
                    <div class="col-md-4">
                        <fieldset>
                            <div class="highcharts-figure">
                                <div id="container"></div>

                            </div>
                        </fieldset>
                    </div>
                    <div class="col-md-4">
                        <fieldset>
                            <figure class="highcharts-figure">
                                <div id="container2"></div>

                            </figure>
                        </fieldset>
                    </div>
                    <div class="col-md-4">
                        <fieldset>
                            <div class="highcharts-figure">
                                <div id="container3"></div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="col-md-4">
                        <iframe width="600" height="450" src="https://lookerstudio.google.com/embed/reporting/859be746-478d-4be1-92e6-af502889a5cc/page/DntyD" 
                            frameborder="0" style="border:0" 
                             sandbox="allow-storage-access-by-user-activation allow-scripts allow-same-origin allow-popups allow-popups-to-escape-sandbox"></iframe>
                    </div>

                </div>


            </div>
        </section>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
    <script src="https://code.highcharts.com/modules/export-data.js"></script>
    <script src="https://code.highcharts.com/modules/accessibility.js"></script>
    <div runat="server" id="divscript"></div>
    <div runat="server" id="divscript2"></div>


    <script>
        Highcharts.chart('container3', {
            chart: {
                type: 'bar'
            },
            title: {
                text: '',
                align: 'left'
            },

            xAxis: {
                categories: ['Africa', 'America', 'Asia', 'Asia', 'Asia', 'Asia', 'Asia', 'Asia', 'Asia', 'Asia'],
                title: {
                    text: null
                },
                gridLineWidth: 1,
                lineWidth: 0
            },
            yAxis: {
                min: 0,
                title: {
                    text: '',
                    align: 'high'
                },
                labels: {
                    overflow: 'justify'
                },
                gridLineWidth: 0
            },
            tooltip: {
                valueSuffix: ' millions'
            },
            plotOptions: {
                bar: {
                    borderRadius: '50%',
                    dataLabels: {
                        enabled: true
                    },
                    groupPadding: 0.1
                }
            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'top',
                x: -40,
                y: 80,
                floating: true,
                borderWidth: 1,
                backgroundColor:
                    Highcharts.defaultOptions.legend.backgroundColor || '#FFFFFF',
                shadow: true
            },
            credits: {
                enabled: false
            },
            series: [{
                name: 'Year 1990',
                data: [631, 727, 3202, 721, 721, 721, 721, 721, 721, 721]
            }, {
                name: 'Year 2000',
                data: [814, 841, 3714, 726, 726, 726, 726, 726, 726, 726]
            }, {
                name: 'Year 2018',
                data: [1276, 1007, 4561, 746, 746, 746, 746, 746, 746, 746]
            }]
        });

    </script>
</asp:Content>

