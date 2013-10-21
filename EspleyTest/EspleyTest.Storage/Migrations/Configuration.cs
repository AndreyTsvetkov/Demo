using EspleyTest.Domain;

namespace EspleyTest.Storage.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EspleyTest.Storage.ResumeRepository>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EspleyTest.Storage.ResumeRepository context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
	        context.Resumes.AddOrUpdate(
		        p => p.ApplicantName,
		        new Resume
			        {
				        ApplicantName = "������� ������� ����������",
				        Id = 6093,
				        LastUpdated = new DateTime(2013, 01, 01),
				        HtmlBody = @"<div class=""ofl_h"">
    
    
    
    
    
    <div class=""mrl25"">
        <div class=""l mrr20 h140"">
            <div>
                
                    <img id=""ResumePhotoMain_6093"" alt="""" class=""gr_b cr_b"" src=""/company/getimage?file=/Content/uploads/noimg.gif&amp;width=120&amp;height=120&amp;scale=n"">
                    
            </div>
            
        </div>
        <div class=""mrb20"">
            <h2 class=""hl fs18 n_bd"">
                ������� ������� ����������
            </h2>
            
            <div class=""mrt6"">
                �������������� ����������, �������� 
            </div>
            
            <div class=""c_lbl"">
                �� ��� ������
            </div>
            
            <div class=""mrt10"">
                
                ���� ��������:
                12/12/1983 (29)
                
            </div>
            <div class=""mrt3"">
                ������, ��������� �������, ����������
            </div>
            
            <div class=""mrt3"">
                �������� ���������:
                �����/ �������
            </div>
            
            <div class=""gr mrt3 fs12"">
                ���� ���������� ���������� ������:
                19/10/2013</div>
            
        </div>
    </div>
    
    
    <div class=""c"">
    </div>
    <div class=""mrl25"">
        
        <div class=""l mrr10 hl_a"">
            
            <a id=""aSendVacancy"" class=""blk h20 pdt5 bg_chl pdb2 no_b cr_b no_u tac br_no bp_i_5_7 message_sm pdl20 pdr5 mspdl0 mspdr0 b"" href=""javascript:void(0);"" onclick=""$('#bSendVacancy').dialog('open')"">����������
                ��������</a>
        </div>
    </div>
    
    
    <script type=""text/javascript"">
        $(document).ready(function () {
            if ($('#bSendVacancy').length != 0) {
                $('#bSendVacancy').dialog({
                    autoOpen: false,
                    width: 500,
                    modal: true,
                    resizable: false,
                    buttons: {
                        ""�������"": function () {
                            $(this).dialog(""close"");
                        }
                    }
                });
            }
        });
    </script>
    
    
    
    <div class=""c"">
    </div>
    
    
    <div class=""mrr250 ofl_h mrt20"" defcl=""mrr250"" mincl=""mrr50"">
        <div>
            <div class=""hl_r r"">
            </div>
            <div class=""hl_l l"">
            </div>
            <div class=""hl_c pdl25"">
                <h2 class=""hl n_bd lh30"">
                    ���������������� ��������</h2>
            </div>
            <div class=""c br_l_bl br_r_bl pd10 lh15 pdl25"">
                
                <div class=""l w250"">
                    ��������� ����� ������������:</div>
                <div class=""mrl270"">
                    �������������� ����������, �������� </div>
                <div class=""c"">
                </div>
                
                <div class=""mrt10"">
                    <div class=""l w250"">
                        ��������� ���������/�����:</div>
                    
                    
                    <div class=""mrl270"">
                        ���-�����������, �����������, ����������� ��� ������
                    </div>
                </div>
                
                <div class=""c"">
                </div>
                
                <div class=""mrt10"">
                    <div class=""l w250"">
                        ��������� ������ ������:</div>
                    <div class=""mrl270"">
                        ������ ������� ����</div>
                </div>
                
                <div class=""c"">
                </div>
                
                <div class=""mrt10"">
                    <div class=""l w250"">
                        ����� �� � �������� � ������ �����/������:</div>
                    <div class=""mrl270"">
                        ���</div>
                </div>
                <div class=""c"">
                </div>
                
                <div class=""mrt10"">
                    <div class=""l w250"">
                        ����� �� � �������������:</div>
                    <div class=""mrl270"">
                        ��</div>
                </div>
                <div class=""c"">
                </div>
                
                <div class=""mrt10"">
                    <div class=""l w250"">
                        �������� ���������� �����:</div>
                    <div class=""mrl270"">
                        1&nbsp;000&nbsp;000 ���</div>
                </div>
                <div class=""c"">
                </div>
            </div>
            <div class=""c top_v_b"">
                <div class=""r hl_rb"">
                </div>
                <div class=""l hl_lb"">
                </div>
            </div>
        </div>
        
        <div class=""c"">
        </div>
        <div class=""mrt20 mrr10 mrl25"">
            
            <h2 class=""hl n_bd mrb10"">
                ���� ������</h2>
            
            <div class=""c"">
            </div>
            <div class=""mrt10"">
                <div class=""l w250"">
                    07/2012 - ����. �����
                </div>
                <div class=""mrl270 lh15"">
                    
                    <div class=""b"">
                        �����������
                    </div>
                    
                    <div>
                        <span class=""b"">����������</span> (��������� �������, ����������)
                    </div>
                    
                    <div>
                        �������������� ����������, �������� 
                    </div>
                    
                    <div class=""mrt10 itc b"">
                        �����������
                    </div>
                    - �������� ��� ���������� ""������� ������������� �������������� ����������"" (���������� ������������ ������������������ ������ - ����), ������� �������� � ���� ��������� ������:<br>1. ������������� �����<br>2. �������� ����������<br>3. �������� �������<br>4. ���������������<br>5. ������������� ���������<br>6. ���������� ����� � ������ ��� ��������<br>7. ����� ���������� � ����������<br>����������: PHP (Symfony framework), JS (EXTJS 4.2), Oracle<br><br>- ���������� ���������� ""����� ����"" ��� ���.����������
                    
                </div>
            </div>
            
            <div class=""c"">
            </div>
            <div class=""mrt10"">
                <div class=""l w250"">
                    01/2012 - ����. �����
                </div>
                <div class=""mrl270 lh15"">
                    
                    <div class=""b"">
                        ��� �����������
                    </div>
                    
                    <div>
                        <span class=""b"">�������</span> (��������� �������, ����������)
                    </div>
                    
                    <div>
                        �������������� ����������, �������� 
                    </div>
                    
                    <div class=""mrt10 itc b"">
                        �����������
                    </div>
                    1. http://vkrgusev.ts6.ru/ - ��� ���������� ��� ��������� ������ ���.����������<br>2. http://vov-museum.ru/ - ���� ����� ������� ������������� ����� ��� ��������� ������<br>3. http://studclubifmo.ru/ - �������������� ������ ��� ���������� ������������ ���������<br>4. http://teplodomys.ru/<br>5. http://livadiyabukhara.uz/
                    
                </div>
            </div>
            
            <div class=""c"">
            </div>
            <div class=""mrt10"">
                <div class=""l w250"">
                    12/2012 - 05/2013
                </div>
                <div class=""mrl270 lh15"">
                    
                    <div class=""b"">
                        �����������
                    </div>
                    
                    <div>
                        <span class=""b"">Axioma</span> (���� (��������), ������)
                    </div>
                    
                    <div>
                        �������������� ����������, �������� 
                    </div>
                    
                    <div class=""mrt10 itc b"">
                        �����������
                    </div>
                    - ��� ���� ������� ""Classicus"" - http://www.classicus.ru/  (Symfony Framework 1.4)<br>- ���������� ���� http://2dinner.com (Symfony Framework 1.4)
                    
                </div>
            </div>
            
            <div class=""c"">
            </div>
            <div class=""mrt10"">
                <div class=""l w250"">
                    10/2010 - 11/2011
                </div>
                <div class=""mrl270 lh15"">
                    
                    <div class=""b"">
                        �����������
                    </div>
                    
                    <div>
                        <span class=""b"">�� �������� ������</span> (��������� �������, ����������)
                    </div>
                    
                    <div>
                        �������������� ����������, �������� 
                    </div>
                    
                    <div class=""mrt10 itc b"">
                        �����������
                    </div>
                    �������� ����� ���������� ����� ""��������"", <br>�������� ����� ���������� ���� ��������� �������,<br>�������� ��� ���������� ������ ���������� ����� ��������� � ������������� ������ ��������� �������
                    
                </div>
            </div>
            
        </div>
        <div class=""c"">
        </div>
        <div class=""dt mrt20"">
        </div>
        
        <div class=""c"">
        </div>
        <div class=""mrt20 mrr10 mrl25"">
            
            <h2 class=""hl n_bd mrb10"">
                �����������</h2>
            
            <div class=""c"">
            </div>
            <div class=""mrt10"">
                <div class=""l w250"">
                    09/2002 - 08/2006
                </div>
                <div class=""mrl270 lh15"">
                    <div class=""b"">
                        ��������� ��������������� �������� ������� � ������ ��������������, ������ ������
                    </div>
                    <div>
                        ������� , �������� (������)
                    </div>
                    
                </div>
            </div>
            
        </div>
        <div class=""c"">
        </div>
        <div class=""dt mrt20"">
        </div>
        
        <div class=""c"">
        </div>
        <div class=""mrt20 mrr10 mrl25"">
            
            <h2 class=""hl n_bd mrb10"">
                ������</h2>
            
            <div class=""mrt10"">
                <div class=""l w250"">
                    ������ ����������:
                </div>
                <div class=""mrl270"">
                    �������
                </div>
            </div>
            
            <div class=""mrt10"">
                <div class=""l w250"">
                    ������������ ������:
                </div>
                <div class=""mrl270"">
                    HTML, CSS, JavaScript, JQuery, ExtJS, Dojo, PHP, Symfony
                </div>
            </div>
            
            <div class=""mrt10"">
                <div class=""l w250"">
                    ������ ������:
                </div>
                <div class=""mrl270"">
                    <div>���������� - ���������;</div><div>������� - ��������� ��������</div>
                </div>
            </div>
            
            <div class=""mrt10"">
                <div class=""l w250"">
                    ������������ �������������:
                </div>
                <div class=""mrl270"">
                     B
                </div>
            </div>
            
        </div>
        <div class=""c"">
        </div>
        <div class=""dt mrt20"">
        </div>
        
        <div class=""mrt20 mrr10 mrl25"">
            <h2 class=""hl n_bd mrb10"">
                �������� / �����</h2>
            <div class=""mrt10 lh15"">
                ������ �������� ���))
            </div>
        </div>
        <div class=""c"">
        </div>
        <div class=""dt mrt20"">
        </div>
        
        <div class=""c"">
        </div>
        <div class=""mrt20 mrr10 mrl25"">
            
            <h2 class=""hl n_bd mrb10"">
                ���������� ����������</h2>
            
            <div class=""mrt10"">
                <div class=""l w250"">
                    ��� (���):
                </div>
                <div class=""mrl270"">
                    
                    +99893 6554583
                </div>
            </div>
            <div class=""c"">
            </div>
            
            <div class=""mrt10"">
                <div class=""l w250"">
                    ��� (���):
                </div>
                <div class=""mrl270"">
                    +99865 2260737
                </div>
            </div>
            <div class=""c"">
            </div>
            
            <div class=""mrt10"">
                <div class=""l w250"">
                    �������������� �������:
                </div>
                <div class=""mrl270"">
                    +99865 2261248
                </div>
            </div>
            <div class=""c"">
            </div>
            
            <div class=""mrt10"">
                <div class=""l w250"">
                    Email:
                </div>
                <div class=""mrl270"">
                    <a href=""mailto:xnemo12@mail.ru"" class=""u c_lgbl"">
                        xnemo12@mail.ru</a>
                </div>
            </div>
            <div class=""c"">
            </div>
            
            <div class=""mrt10"">
                <div class=""l w250"">
                    ������ ��������:
                </div>
                <div class=""mrl270"">
                    Skype: xnemo12
                </div>
            </div>
            
        </div>
    </div>
    <div class=""c"">
    </div>
    
    <div class=""c"">
    </div>
    
    
    
    
    
    
</div>"
			        }
		        );
	        //
        }
    }
}
