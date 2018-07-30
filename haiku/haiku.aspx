<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="haiku.aspx.cs" Inherits="haiku.haiku" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div class="box1">
            <h3 style="color: #AA00FF;">Haiku Generator:
                <br />
                ★★★★★★★★★★★
            </h3>
            <br />
            <asp:Label ID="label_haiku1" runat="server"></asp:Label>
            <br />
            <asp:Label ID="label_haiku2" runat="server"></asp:Label>
            <br />
            <asp:Label ID="label_haiku3" runat="server"></asp:Label>
            <br />
            <br />
            <div runat="server" id="div_accent" class="accent_haiku">
                <br />
                <asp:Label ID="label_saveq" Visible="false" runat="server"><i>Is it brilliant? Add it to the guestbook.</i></asp:Label>
                <br />
                <br />
                <asp:TextBox ID="text_initials" runat="server" MaxLength="3" Text="Enter your initials" Visible="False"></asp:TextBox>
                <asp:Button ID="btn_saveq" Visible="false" runat="server" OnClick="btn_saveq_Click" Text="Save to Guestbook" Style="margin-left: 59px" />
                <br />
                <br />
            </div>
            <br />
            <br />
            <asp:Button ID="btn_generate" runat="server" OnClick="btn_generate_Click" Text="Generate Haiku" CausesValidation="False" />
        </div>
        <br />
        <img class="img-haiku" src="Images/a1.png" alt="just some graphic" title="2018" />

        <div class="box2">
            <h3 style="color: #AA00FF;">Guestbook:                
                <br />
                ★★★★★★★★★★★
            </h3>
            <br />
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DataList CssClass="haikulist" Autopostback="true" ID="DataList_gl" runat="server" DataKeyField="ID" DataSourceID="haikus_saved">
                        <ItemTemplate>
                            <asp:Label ID="line1Label" runat="server" Text='<%# Eval("line1") %>' />
                            <br />
                            <asp:Label ID="line2Label" runat="server" Text='<%# Eval("line2") %>' />
                            <br />
                            <asp:Label ID="line3Label" runat="server" Text='<%# Eval("line3") %>' />
                            <br />
                            <br />
                            <asp:Label CssClass="haikuInfoLabel" ID="authorLabel" runat="server" Text='<%# Eval("author") + ", " + Eval("entrydate", "{0:d}") %>' />
                            <br />
                            <asp:Button CssClass="votebtn" ID="btn_voteup" runat="server" Text="▲" CommandArgument='<%#Eval("ID")+","+ Eval("votes")%>' CommandName="upClick" OnClick="votebtn_Click" CausesValidation="False" />
                            <asp:Label CssClass="haikuInfoLabel" ID="votesLabel" runat="server" Text='<%# Eval("votes")%>' />
                            <asp:Button CssClass="votebtn" ID="btn_votedown" runat="server" Text="▼" CommandArgument='<%#Eval("ID")+","+ Eval("votes")%>' CommandName="downClick" OnClick="votebtn_Click" CausesValidation="False" />
                        </ItemTemplate>
                        <SeparatorTemplate>
                            <br />
                            ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
                            <br /><br />
                        </SeparatorTemplate>
                    </asp:DataList>
                    <asp:SqlDataSource ID="haikus_saved" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [haikus] ORDER BY [votes] DESC, [entrydate] DESC"></asp:SqlDataSource>
        
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger EventName="Click" ControlID="btn_saveq" />
                </Triggers>
            </asp:UpdatePanel>

             <br />
            <br />
            <br />
        </div>
        <br />
    </div>
    <br />
    <asp:Label ID="label_notice" CssClass="haikuInfoLabel" runat="server" Text="Label">*Note: The dictionary was aquired from an open-source project and is still in the process of being edited.</asp:Label>
    <br />
    <br />
    <asp:Panel ID="Panel1" runat="server" CssClass="footer">
        Teah Elaschuk 2018 - Haiku Generator v1.4.1 [<a href="cl-haiku.html" style="font-size: smaller;">changelog</a>]
    </asp:Panel>


</asp:Content>
