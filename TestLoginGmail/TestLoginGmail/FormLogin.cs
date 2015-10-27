using System;
using System.Net;
using System.Text;
using System.Xml;
using DevExpress.XtraEditors;

namespace TestLoginGmail
{
    public partial class FormLogin : XtraForm
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender,
                                 EventArgs e)
        {
            dxErrorProvider1.ClearErrors();
            if(string.IsNullOrEmpty(txtUsername.Text))
            {
                dxErrorProvider1.SetError(txtUsername,
                                          "Username cannot blank");
            }
            if(string.IsNullOrEmpty(txtPassword.Text))
            {
                dxErrorProvider1.SetError(txtPassword,
                                          "Password cannot blank");
            }
            if(!dxErrorProvider1.HasErrors)
            {
                WebClient objClient = new WebClient();
                XmlDocument doc = new XmlDocument();
                try
                {
                    objClient.Credentials = new NetworkCredential(txtUsername.Text,
                                                                  txtPassword.Text);
                    var response = Encoding.UTF8.GetString(objClient.DownloadData("https://mail.google.com/mail/feed/atom"));
                    response = response.Replace("<feed version=\"0.3\" xmlns=\"http://purl.org/atom/ns#\">",
                                                "<feed>");
                    doc.LoadXml(response);
                    var node = doc.SelectSingleNode("/feed/fullcount");
                    if(node != null)
                    {
                        GlobalVariables.mailCount = Convert.ToInt32(node.InnerText);
                        if(GlobalVariables.mailCount > 0)
                        {
                            var nodeList = doc.SelectNodes("/feed/entry");
                            if(nodeList != null)
                            {
                                foreach (XmlNode node2 in nodeList)
                                {
                                    var xmlNode = node2.ChildNodes.Item(0);
                                    if(xmlNode != null)
                                    {
                                        GlobalVariables.emailMessages.Add(xmlNode.InnerText);
                                    }
                                    var item = node2.ChildNodes.Item(6);
                                    if(item != null)
                                    {
                                        GlobalVariables.emailFrom.Add(item.ChildNodes[0].InnerText);
                                    }
                                    GlobalVariables.tempCounter++;
                                }
                            }
                            GlobalVariables.tempCounter = 0;
                        }
                        Hide();
                        Form1 frm = new Form1();
                        frm.Show();
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                }
            }
        }
    }
}
