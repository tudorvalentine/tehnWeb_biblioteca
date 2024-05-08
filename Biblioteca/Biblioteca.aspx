<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Biblioteca.aspx.cs" Inherits="Biblioteca.WebForm1" %>

<!DOCTYPE html>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Biblioteca</h1>
            <asp:GridView ID="gvBooks" runat="server" AutoGenerateColumns="False" OnRowEditing="gvBooks_RowEditing"
                OnRowCancelingEdit="gvBooks_RowCancelingEdit" OnRowUpdating="gvBooks_RowUpdating" OnRowDeleting="gvBooks_RowDeleting"
                DataKeyNames="Id">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" />
                    <asp:BoundField DataField="title" HeaderText="Titlu" />
                    <asp:BoundField DataField="author" HeaderText="Autor" />
                    <asp:BoundField DataField="year_pub" HeaderText="An Publicare" />
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
            <br />
            <h2>Adăugare carte
                </h2>
               <table>
                   <tr>
                       <td>Titlu:</td>
                       <td><asp:TextBox ID="txtTitlu" runat="server"></asp:TextBox></td>
                   </tr>
                   <tr>
                       <td>Autor:</td>
                       <td><asp:TextBox ID="txtAutor" runat="server"></asp:TextBox></td>
                   </tr>
                   <tr>
                       <td>An Publicare:</td>
                       <td><asp:TextBox ID="txtAnPublicare" runat="server"></asp:TextBox></td>
                   </tr>
                   <tr>
                       <td colspan="2">
                           <asp:Button ID="btnSave" runat="server" Text="Salvează" OnClick="btnSave_Click" />
                       </td>
                   </tr>
               </table>
           </div>
       </form>
   </body>
   </html>