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
				        ApplicantName = "Джураев Джамшид Мухтарович",
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
                Джураев Джамшид Мухтарович
            </h2>
            
            <div class=""mrt6"">
                Информационные технологии, Интернет 
            </div>
            
            <div class=""c_lbl"">
                не ищу работу
            </div>
            
            <div class=""mrt10"">
                
                Дата рождения:
                12/12/1983 (29)
                
            </div>
            <div class=""mrt3"">
                Бухара, Бухарская область, Узбекистан
            </div>
            
            <div class=""mrt3"">
                Семейное положение:
                женат/ замужем
            </div>
            
            <div class=""gr mrt3 fs12"">
                Дата последнего обновления резюме:
                19/10/2013</div>
            
        </div>
    </div>
    
    
    <div class=""c"">
    </div>
    <div class=""mrl25"">
        
        <div class=""l mrr10 hl_a"">
            
            <a id=""aSendVacancy"" class=""blk h20 pdt5 bg_chl pdb2 no_b cr_b no_u tac br_no bp_i_5_7 message_sm pdl20 pdr5 mspdl0 mspdr0 b"" href=""javascript:void(0);"" onclick=""$('#bSendVacancy').dialog('open')"">Предложить
                вакансию</a>
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
                        ""Закрыть"": function () {
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
                    Профессиональные ожидания</h2>
            </div>
            <div class=""c br_l_bl br_r_bl pd10 lh15 pdl25"">
                
                <div class=""l w250"">
                    Ожидаемая сфера деятельности:</div>
                <div class=""mrl270"">
                    Информационные технологии, Интернет </div>
                <div class=""c"">
                </div>
                
                <div class=""mrt10"">
                    <div class=""l w250"">
                        Ожидаемая должность/сфера:</div>
                    
                    
                    <div class=""mrl270"">
                        Веб-программист, Программист, Разработчик баз данных
                    </div>
                </div>
                
                <div class=""c"">
                </div>
                
                <div class=""mrt10"">
                    <div class=""l w250"">
                        Ожидаемый график работы:</div>
                    <div class=""mrl270"">
                        Полный рабочий день</div>
                </div>
                
                <div class=""c"">
                </div>
                
                <div class=""mrt10"">
                    <div class=""l w250"">
                        Готов ли к переезду в другой город/страну:</div>
                    <div class=""mrl270"">
                        Нет</div>
                </div>
                <div class=""c"">
                </div>
                
                <div class=""mrt10"">
                    <div class=""l w250"">
                        Готов ли к командировкам:</div>
                    <div class=""mrl270"">
                        Да</div>
                </div>
                <div class=""c"">
                </div>
                
                <div class=""mrt10"">
                    <div class=""l w250"">
                        Желаемая заработная плата:</div>
                    <div class=""mrl270"">
                        1&nbsp;000&nbsp;000 сум</div>
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
                Опыт работы</h2>
            
            <div class=""c"">
            </div>
            <div class=""mrt10"">
                <div class=""l w250"">
                    07/2012 - наст. время
                </div>
                <div class=""mrl270 lh15"">
                    
                    <div class=""b"">
                        Разработчик
                    </div>
                    
                    <div>
                        <span class=""b"">Бухарасофт</span> (Бухарская область, Узбекистан)
                    </div>
                    
                    <div>
                        Информационные технологии, Интернет 
                    </div>
                    
                    <div class=""mrt10 itc b"">
                        Обязанности
                    </div>
                    - Создание веб приложения ""Система автоматизации корпоративного управления"" (Корпоратив бошкарувнинг автоматлаштирилган тизими - КБАТ), который включает в себе следующие модули:<br>1. Корпоративная почта<br>2. Контроль дисциплины<br>3. Контроль заданий<br>4. Документооборот<br>5. Корпоративный календарь<br>6. Телефонная книга и модуль СМС рассылки<br>7. Архив документов и справочник<br>Технологии: PHP (Symfony framework), JS (EXTJS 4.2), Oracle<br><br>- Справочное приложение ""Ягона ойна"" для гос.учреждений
                    
                </div>
            </div>
            
            <div class=""c"">
            </div>
            <div class=""mrt10"">
                <div class=""l w250"">
                    01/2012 - наст. время
                </div>
                <div class=""mrl270 lh15"">
                    
                    <div class=""b"">
                        Веб программист
                    </div>
                    
                    <div>
                        <span class=""b"">Фриланс</span> (Бухарская область, Узбекистан)
                    </div>
                    
                    <div>
                        Информационные технологии, Интернет 
                    </div>
                    
                    <div class=""mrt10 itc b"">
                        Обязанности
                    </div>
                    1. http://vkrgusev.ts6.ru/ - Веб приложение для генерации таблиц физ.упражнений<br>2. http://vov-museum.ru/ - Сайт музей Великой Отечественной Войны для дипломной работы<br>3. http://studclubifmo.ru/ - Информационный ресурс для проведения студенческих конкурсов<br>4. http://teplodomys.ru/<br>5. http://livadiyabukhara.uz/
                    
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
                        Разработчик
                    </div>
                    
                    <div>
                        <span class=""b"">Axioma</span> (Рига (удаленна), Латвия)
                    </div>
                    
                    <div>
                        Информационные технологии, Интернет 
                    </div>
                    
                    <div class=""mrt10 itc b"">
                        Обязанности
                    </div>
                    - Веб сайт клиники ""Classicus"" - http://www.classicus.ru/  (Symfony Framework 1.4)<br>- Социальная сеть http://2dinner.com (Symfony Framework 1.4)
                    
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
                        Программист
                    </div>
                    
                    <div>
                        <span class=""b"">Ёш дастурчи Бухоро</span> (Бухарская область, Узбекистан)
                    </div>
                    
                    <div>
                        Информационные технологии, Интернет 
                    </div>
                    
                    <div class=""mrt10 itc b"">
                        Обязанности
                    </div>
                    Создание сайта кредитного союза ""Табассум"", <br>Создание сайта управления ССПО Бухарской области,<br>Создание веб приложения обмена документов среди колледжей и академических лицеев Бухарской области
                    
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
                Образование</h2>
            
            <div class=""c"">
            </div>
            <div class=""mrt10"">
                <div class=""l w250"">
                    09/2002 - 08/2006
                </div>
                <div class=""mrl270 lh15"">
                    <div class=""b"">
                        Бухарский Технологический институт пищевой и легкой промышленности, Касбий таълим
                    </div>
                    <div>
                        Инженер , Бакалавр (Высшее)
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
                Навыки</h2>
            
            <div class=""mrt10"">
                <div class=""l w250"">
                    Знание компьютера:
                </div>
                <div class=""mrl270"">
                    Эксперт
                </div>
            </div>
            
            <div class=""mrt10"">
                <div class=""l w250"">
                    Компьютерные навыки:
                </div>
                <div class=""mrl270"">
                    HTML, CSS, JavaScript, JQuery, ExtJS, Dojo, PHP, Symfony
                </div>
            </div>
            
            <div class=""mrt10"">
                <div class=""l w250"">
                    Знание языков:
                </div>
                <div class=""mrl270"">
                    <div>Английский - Начальный;</div><div>Русский - Свободное владение</div>
                </div>
            </div>
            
            <div class=""mrt10"">
                <div class=""l w250"">
                    Водительское удостоверение:
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
                Интересы / хобби</h2>
            <div class=""mrt10 lh15"">
                Писать красивый код))
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
                Контактная информация</h2>
            
            <div class=""mrt10"">
                <div class=""l w250"">
                    Тел (моб):
                </div>
                <div class=""mrl270"">
                    
                    +99893 6554583
                </div>
            </div>
            <div class=""c"">
            </div>
            
            <div class=""mrt10"">
                <div class=""l w250"">
                    Тел (дом):
                </div>
                <div class=""mrl270"">
                    +99865 2260737
                </div>
            </div>
            <div class=""c"">
            </div>
            
            <div class=""mrt10"">
                <div class=""l w250"">
                    Дополнительный телефон:
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
                    Другие контакты:
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
