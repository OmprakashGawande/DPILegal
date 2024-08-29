<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="DynamicDashboard.aspx.cs" Inherits="Legal_DynamicDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <iframe src="https://lookerstudio.google.com/embed/reporting/859be746-478d-4be1-92e6-af502889a5cc/page/p_hwirtdpygd" 
                    style="top: 5px; left: 0; width: 100%; height: 1000px; position: inherit; border: 0;"></iframe>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
</asp:Content>