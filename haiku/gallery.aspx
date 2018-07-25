<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="gallery.aspx.cs" Inherits="haiku.gallery" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
    <div class="container">
        <div class="row">
            <img src="Images/19.png" title="2017" />
            <img src="Images/15.png" title="2016" />
            <img src="Images/7.png" title="2016" />
        </div>
        <div class="row">
            <img src="Images/14.PNG" title="2016" />
            <img src="Images/2.png" title="2016" />
            <img src="Images/3.png" title="2016" />
        </div>
        <div class="row">
            <hr />
            <div class="special-text">
                <p>More: </p>
                <a href="/gallery-digital.aspx"> digital </a>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <a href="/wip.aspx"> nick </a>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <a href="/wip.aspx"> nightshifts </a>

            </div>
            <img src="Images/22.png" class="special" title="2018" />
            <hr />
        </div>
        <div class="row">
            <img src="Images/21.png" title="2018" />
            <img src="Images/23.png" title="2018" />
            <img src="Images/6.png" title="2017" />
        </div>
        <div class="row">
            <img src="Images/12.png" title="2017" />
            <img src="Images/1.png" title="2016" />
            <img src="Images/10.png" title="2016" />
        </div>
        <div>
            <hr />
            <img src="Images/16.png" title="2018" />
            <img src="Images/11.png" title="2016" />
            <img src="Images/8.png" class="bigger" title="2016" />
            <img src="Images/9.png" title="2016" />
            <img src="Images/4.png" title="2016" />
        </div>
        <!--
     <img src="Images/20.png" title="2018" />
     <img src="Images/13.jpg" title="2018" />
     <img src="Images/5.png" title="2015" />
        -->
    </div>
</asp:Content>
