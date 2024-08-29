﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="MasterReport.aspx.cs" Inherits="Legal_MasterReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../DataTable_CssJs/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../DataTable_CssJs/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="../DataTable_CssJs/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="../Main_plugins/bootstrap/css/bootstrap-multiselect.css" rel="stylesheet" />
    <style>
        .label-orange {
            background-color: #f5ac45;
        }

        .label {
            display: inline;
            padding: 0.2em 0.6em 0.3em;
            font-size: 80%;
            font-weight: 700;
            line-height: 1;
            color: #fff;
            text-align: center;
            white-space: nowrap;
            vertical-align: baseline;
            border-radius: 0.25em;
        }

        a.btn.btn-default.buttons-excel.buttons-html5 {
            background: #066205;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
            margin-top: 4%;
        }

        a.btn.btn-default.buttons-print {
            background: #1e79e9;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
            margin-top: 4%;
        }

        th.sorting, th.sorting_asc, th.sorting_desc {
            background: teal !important;
            color: white !important;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
        }

        a.btn.btn-default.buttons-excel.buttons-html5 {
            background: #ff5722c2;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-pdf.buttons-html5 {
            background: #009688c9;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-print {
            background: #e91e639e;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
        }

            a.btn.btn-default.buttons-print:hover, a.btn.btn-default.buttons-pdf.buttons-html5:hover, a.btn.btn-default.buttons-excel.buttons-html5:hover {
                box-shadow: 1px 1px 1px #808080;
            }

            a.btn.btn-default.buttons-print:active, a.btn.btn-default.buttons-pdf.buttons-html5:active, a.btn.btn-default.buttons-excel.buttons-html5:active {
                box-shadow: 1px 1px 1px #808080;
            }

        .box.box-pramod {
            border-top-color: #1ca79a;
        }

        .box {
            min-height: auto;
        }

        .sorting,
        .sorting_asc,
        .sorting_desc,
        .sorting_asc_disabled,
        .sorting_desc_disabled {
            cursor: pointer;
            position: relative;
            &:after


        {
            position: absolute;
            bottom: 8px;
            right: 8px;
            display: block;
            font-family: 'Glyphicons Halflings';
            opacity: 0.5;
        }

        }

        .sorting:after {
            opacity: 0.2;
            content: "⏭" !important; /* sort */
        }

        .sorting_asc:after {
            content: "⏬" !important; /* sort-by-attributes */
        }

        .sorting_desc:after {
            content: "⏫" !important; /* sort-by-attributes-alt */
        }
    </style>
    <style>
        label {
            font-size: 15px;
        }

        .aspNetDisabled {
            pointer-events: none;
            opacity: 0.6;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <asp:ValidationSummary ID="VDS" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="card">
                    <div class="card-header">
                        Master Report
                    </div>
                    <div class="card-body">
                        <fieldset>
                            <legend>Search Case List</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Court Name <%--<span style="color: red;"><b>*</b></span>
                                            <asp:RequiredFieldValidator ID="rfvCaseyear" ValidationGroup="Save"
                                                ErrorMessage="Select Court Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="ddlCourtName" Display="Dynamic" runat="server" InitialValue="0">
                                            </asp:RequiredFieldValidator><br />--%>
                                        </label>
                                        <asp:DropDownList ID="ddlCourtName" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Case type<%--<span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Save"
                                                ErrorMessage="Select Case type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="ddlCasetype" Display="Dynamic" runat="server" InitialValue="0">
                                            </asp:RequiredFieldValidator><br />--%>
                                        </label>
                                        <asp:DropDownList ID="ddlCasetype" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Case Year<span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Save"
                                                ErrorMessage="Select Case Year." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="ddlCaseYear" Display="Dynamic" runat="server" InitialValue="0">
                                            </asp:RequiredFieldValidator><br />
                                        </label>
                                        <asp:DropDownList ID="ddlCaseYear" runat="server" CssClass="form-control select2"></asp:DropDownList>
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
                                <div class="col-md-3" style="margin-top: 2rem">
                                    <div class="row">
                                        <div class="col-md-5">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-block" ValidationGroup="Save" Text="Search" OnClick="btnSearch_Click" />
                                        </div>
                                        <div class="col-md-5">
                                            <a href="MasterReport.aspx" class="btn btn-default btn-block">Clear</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>Report</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="GrdMasterReport" runat="server" CssClass="datatable table table-bordered" EmptyDataText="NO RECORD FOUND" AutoGenerateColumns="false" DataKeyNames="Case_ID">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    <asp:Label ID="lblCaseId" runat="server" Text='<%# Eval("Case_ID") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Case No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCaseNO" runat="server" Text='<%# Eval("CaseNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="District">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldistrict" runat="server" Text='<%# Eval("District_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Petitioner Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPetitionerName" runat="server" Text='<%# Eval("PetitonerName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesignation" runat="server" Text='<%# Eval("Designation_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Section">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSection" runat="server" Text='<%# Eval("Section") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Respondent Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRespondentName" runat="server" Text='<%# Eval("RespondentName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HOD Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblhod" runat="server" Text='<%# Eval("HodName1") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Case Subject">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCaseSubject" runat="server" Text='<%# Eval("CaseSubject") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Case Sub Subject">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCaseSubSubject" runat="server" Text='<%# Eval("CaseSubSubject") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Case Matter Detail/Remark">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCaseDetail" runat="server" Text='<%# Eval("CaseDetail") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Case Registration Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcaseRegistrationDate" runat="server" Text='<%# Eval("CaseRegDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Next Hearing Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNextHearingDate" runat="server" Text='<%# Eval("NextHearingDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Return File/Compliance(Yes/No)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReplyCompliance" runat="server" Text='<%# Eval("ActionYesOrNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Return File/Compliance Receipt Number">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReceiptNumber" runat="server" Text='<%# Eval("ReceiptNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Return File/Compliance Receipt Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReceiptDate" runat="server" Text='<%# Eval("ReceiptDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Case Pending at Which Level(DEO/HO/Govt.)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCasePendingwhichlevel" runat="server" Text='<%# Eval("CasePendingwhichlevel") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nodal/OIC District">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOICDistrict_Name" runat="server" Text='<%# Eval("OICDistrict_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nodal Officer/OIC Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOicName" runat="server" Text='<%# Eval("OICName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nodal/OIC Mobile">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOICMobileNo" runat="server" Text='<%# Eval("OICMobileNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nodal/OIC Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOICDesignation_Name" runat="server" Text='<%# Eval("OICDesignation_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="High Priority Case Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHighPriorityCase" runat="server" Text='<%# Eval("HighPriorityCase_Status") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PolicyMatter Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPolicyMatter" runat="server" Text='<%# Eval("PolicyMeterStatus") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="WP Number">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWPNumber" runat="server" Text='<%# Eval("OldCaseNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="WP Year">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWPYear" runat="server" Text='<%# Eval("oldCaseYear") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="WP Case type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbloldCaseType" runat="server" Text='<%# Eval("oldCaseType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="WP Order Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOrderDate" runat="server" Text='<%# Eval("OrderDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Disposed Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCaseDisposal_Date" runat="server" Text='<%# Eval("CaseDisposal_Date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="For Filing WA/RP/SLP due date is">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDateAfter90Days" runat="server" Text='<%# Eval("DateAfter90Days") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remaining days for appeal is">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemainingDays" runat="server" Text='<%# Eval("RemainingDays") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Summary">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOrderSummary" runat="server" Text='<%# Eval("OrderSummary") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Case Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("CaseStatus") %>' ForeColor='<%# Eval("CaseStatus").ToString() == "Pending" || Eval("CaseStatus").ToString() == "pending" ?  System.Drawing.Color.Red :System.Drawing.Color.Green %>' Font-Bold="true"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hypOldCaseDtl" runat="server" Enabled='<%# Eval("DocLink").ToString() == "" ? false : true %>' Target="_blank" NavigateUrl='<%# "~/Legal/OldCaseDocument/" +  Eval("DocLink") %>' CssClass="fa fa-eye"></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>No record Found</EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">

    <script src="../DataTable_CssJs/jquery.js"></script>
    <script src="../DataTable_CssJs/jquery.dataTables.min.js"></script>
    <script src="../DataTable_CssJs/dataTables.bootstrap.min.js"></script>
    <script src="../DataTable_CssJs/dataTables.buttons.min.js"></script>
    <script src="../DataTable_CssJs/buttons.flash.min.js"></script>
    <script src="../DataTable_CssJs/jszip.min.js"></script>
    <script src="../DataTable_CssJs/pdfmake.min.js"></script>
    <script src="../DataTable_CssJs/vfs_fonts.js"></script>
    <script src="../DataTable_CssJs/buttons.html5.min.js"></script>
    <script src="../DataTable_CssJs/buttons.print.min.js"></script>
    <script src="../DataTable_CssJs/buttons.colVis.min.js"></script>
    <script src="../Main_plugins/bootstrap/js/bootstrap-multiselect.js"></script>
    <script type="text/javascript">
        $('.datatable').DataTable({
            paging: true,
            PageLength: 15,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
                '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
                '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: 'Master Report',
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: 'Master Report',
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31]
                    },
                    footer: true
                }],
                dom: {
                    container: {
                        className: 'dt-buttons'
                    },
                    button: {
                        className: 'btn btn-default'
                    }
                }
            }
        });
    </script>

</asp:Content>
