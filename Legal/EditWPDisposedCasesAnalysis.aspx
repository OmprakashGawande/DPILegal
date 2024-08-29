<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="EditWPDisposedCasesAnalysis.aspx.cs" Inherits="Legal_EditWPDisposedCasesAnalysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        label {
            font-size: 15px;
        }

        hr {
            border: 1px solid #2095a1cc;
        }
    </style>
    <style>
        .blink {
            animation: blink 1.5s step-start infinite;
        }

        @keyframes blink {
            0% {
                opacity: 1;
            }

            50% {
                opacity: 0;
            }

            100% {
                opacity: 1;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <asp:ValidationSummary ID="ValidationSummary6" runat="server" ValidationGroup="Save" ShowMessageBox="true" ShowSummary="false" />
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #D9D9D9;">
                        <span class="modal-title" style="float: left" id="myModalLabel">Confirmation</span>
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <p>
                            <%--<img src="../assets/images/question-circle.png" width="30" />--%>&nbsp;&nbsp;
                           <i class="fa fa-question-circle"></i>
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="Button1" OnClick="btnCaseDisposalAnalysis_Click" Style="margin-top: 20px; width: 50px;"/>
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="card">
                    <div class="card-header">
                        <div class="float-left">
                            Edit Case Detail &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%--<asp:Label Class="blink" runat="server" ID="lblFlag" Text="" Style="color: white; font-weight: bolder;"></asp:Label>--%>
                        </div>
                        <div class="float-right">
                            <asp:LinkButton ID="btnBackPage" runat="server" CssClass="btn-sm label-danger" Text="Back"></asp:LinkButton>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <%-- Start Here Bind Case && Petitioner Detail --%>
                                <fieldset id="FieldSet_CaseDetail" runat="server" visible="true">
                                    <legend>Case Info</legend>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Case No.</label>
                                                <asp:TextBox ID="lblCaseNo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Case Year</label>
                                                <asp:DropDownList ID="ddlCaseYear" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Court Name</label>
                                                <asp:DropDownList ID="ddlCourtType" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCourtType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Court Location</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="RfvDistrict" ValidationGroup="CaseDtl"
                                                    ErrorMessage="Select Court Location." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlCourtLocation" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlCourtLocation" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>
                                                    Case Type</label><span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="rfvCasetype" ValidationGroup="Save"
                                                    ErrorMessage="Select Case type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlCasetype" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlCasetype" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Petitioner Name</label>
                                                <asp:TextBox ID="txtPetitionerName" runat="server" CssClass="form-control" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Respondent Name</label>
                                                <asp:TextBox ID="txtRespondentName" runat="server" CssClass="form-control" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Case Dispose type</label>
                                                <asp:TextBox ID="txtCaseDisposeType" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Case Direction type</label>
                                                <asp:TextBox ID="txtOrderWithDirection" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Case Subject</label>
                                                <asp:DropDownList ID="ddlCaseSubject" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCaseSubject_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Case Sub Subject</label>
                                                <asp:DropDownList ID="ddlCaseSubSubject" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Disposed Date</label>
                                                <asp:TextBox ID="txtCaseDisposal_Date" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>For Filing WA/RP/SLP due date is(90 days)</label>
                                                <asp:TextBox ID="txtDateAfter90Days" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Remaining days for appeal is</label>

                                                <asp:TextBox ID="txtRemainingDays" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Order Summary</label>
                                                <asp:TextBox ID="txtOrderSummary" runat="server" CssClass="form-control" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <%-- Start Here Case disposal analysis --%>
                                <fieldset>
                                    <legend>Case Disposal Analysis</legend>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>In favour of Govt or Against Govt</label><span style="color: red"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="Save"
                                                    ErrorMessage="Select In favour of Govt or Against Govt" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlfavGovtAga" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlfavGovtAga" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlfavGovtAga_SelectedIndexChanged">

                                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Favour of Govt"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Against Govt"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%-- If option selected – Against Govt than need to show these two Lawer options  --%>
                                        <div class="col-md-3" runat="server" visible="false" id="DivFirst">
                                            <div class="form-group">
                                                <label>Against Govt</label><span style="color: red"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="rfv2" ValidationGroup="Save"
                                                    ErrorMessage="Select Against Govt" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlAgainstGovt" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlAgainstGovt" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAgainstGovt_SelectedIndexChanged">

                                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Comply the Order"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Go for WA"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%-- If first option selected in the previous step, then: --%>

                                        <div class="col-md-3" runat="server" visible="false" id="DivSecond">
                                            <div class="form-group">
                                                <label>Comply date</label>
                                                <asp:TextBox ID="txtComplydate" runat="server" data-provide="datepicker" placeholder="DD/MM/YYYY" data-date-format="dd/mm/yyyy" data-date-autoclose="true" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" visible="false" id="Divthird">
                                            <div class="form-group">
                                                <label>Present Status Remark</label>
                                                <asp:TextBox ID="txtPresentStatusRemark" runat="server" CssClass="form-control" placeholder="Enter Present Status Remark" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>

                                        <%--If Second option (WA) selected in the previous step, then: --%>

                                        <div class="col-md-3" runat="server" id="Divfourth" visible="false">
                                            <div class="form-group">
                                                <label>WA application date From District To DPI</label>
                                                <asp:TextBox ID="txtWAapplicationdateatDPI" runat="server" data-provide="datepicker" placeholder="DD/MM/YYYY" data-date-format="dd/mm/yyyy" data-date-autoclose="true" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" id="Divfifth" visible="false">
                                            <div class="form-group">
                                                <label>WA application date from legal dept (Govt of MP) </label>
                                                <asp:TextBox ID="txtWAapplicationdatefromlegaldept" runat="server" data-provide="datepicker" placeholder="DD/MM/YYYY" data-date-format="dd/mm/yyyy" data-date-autoclose="true" CssClass="form-control" AutoComplete="off" OnTextChanged="txtWAapplicationdatefromlegaldept_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" id="Divseventh" visible="false">
                                            <div class="form-group">
                                                <label>WA Number and allotted date from HC</label>
                                                <asp:TextBox ID="txtWANumberandallotteddatefromHC" runat="server" data-provide="datepicker" placeholder="DD/MM/YYYY" data-date-format="dd/mm/yyyy" data-date-autoclose="true" CssClass="form-control" AutoComplete="off" OnTextChanged="txtWANumberandallotteddatefromHC_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" id="Divsixth" visible="false">
                                            <div class="form-group">
                                                <label>WA application filling date at HC </label>
                                                <asp:TextBox ID="txtWAapplicationfillingdateatHC" runat="server" data-provide="datepicker" placeholder="DD/MM/YYYY" data-date-format="dd/mm/yyyy" data-date-autoclose="true" CssClass="form-control" AutoComplete="off" OnTextChanged="txtWAapplicationfillingdateatHC_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" id="Diveighth" visible="false">
                                            <div class="form-group">
                                                <label>Days taken for WA application From Case Disposal</label>
                                                <asp:TextBox ID="txtDaystakenforWAPermission" runat="server" ReadOnly="true" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" id="Divninth" visible="false">
                                            <div class="form-group">
                                                <label>
                                                    Days taken Difference From DPI to Govt.<br />
                                                </label>
                                                <asp:TextBox ID="txtDaystakentofiletheapplication" runat="server" ReadOnly="true" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" id="Divtenth" visible="false">
                                            <div class="form-group">
                                                <label>Days taken to File WA after receiving permission from govt.</label>
                                                <asp:TextBox ID="txtFileWAafterrecperfromgovt" runat="server" ReadOnly="true" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6" runat="server" id="Diveleventh" visible="false">
                                            <div class="form-group">
                                                <label>Remark</label>
                                                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" AutoComplete="off" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row justify-content-center">
                                        <div class="col-md-1">
                                            <asp:Button ID="btnSubmit" CssClass="btn btn-block btn-success" ValidationGroup="OldCase" runat="server" Text="Save" OnClick="btnCaseDisposalAnalysis_Click" OnClientClick="return ValidatePage();" />
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
      <script src="../js/ValidationJs.js"></script>
    <script>
        function ValidatePage() {
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('Save');
            }
            if (Page_IsValid) {
                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
    </script>

    <script>
        $('#txtComplydate').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        });
        $('#txtWAapplicationdateatDPI').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        });
        $('#txtWAapplicationdatefromlegaldept').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        });
        $('#txtWAapplicationfillingdateatHC').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        });
        $('#txtWANumberandallotteddatefromHC').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        });
    </script>
</asp:Content>

