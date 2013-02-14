using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Examination.BusinessLogicLayer;
using System.Data.SqlClient;

public partial class Web_UserAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userID"] == null)
        {
            Page.RegisterStartupScript("", "<script>location='../default.aspx';</script>");
        }
        if (Convert.ToInt32(Session["roleID"]) == 5)
        {
            Page.RegisterStartupScript("", "<script>alert('���¼���û�Ȩ�޲�����');location='default.aspx'</script>");
        }
       
    }
    protected void imgBtnSave_Click(object sender, ImageClickEventArgs e)
    {
        if (Page.IsValid)
        {
            Users user = new Users();       //����Users����
            user.UserID = txtUserID.Text.Trim();
            if (!user.CheckUser(user.UserID))//ʹ��CheckUser������֤�û��Ƿ����
            {
                user.UserName = txtUserName.Text;
                user.UserPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(txtUserPwd.Text, "MD5"); 
                user.RoleId = Convert.ToInt16(ddlRole.SelectedValue);
               
                    if (user.InsertByProc())
                    {
                        lblMessage.Text = "�ɹ�������û���Ϣ��";
                        Server.Transfer("UserManage.aspx");
                    }
              else
               {
                   lblMessage.Text = "����û�ʧ�ܣ�";
               }
            }
            else//�û����ڣ�������ʾ
            {
                lblMessage.Text = "���ݿ��д��ھ��иñ�ŵ��û������������룡";
            }
        }
    }
    protected void imgBtnReturn_Click(object sender, ImageClickEventArgs e)
    {
        Server.Transfer("UserManage.aspx");       
    }
}
