using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaxWell.Controls.Common;
using MaxWell.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaxWell.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Eula : ContentPage
    {
        public Eula()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            EulaTextLabel.Effects.Clear();

            EulaTextLabel.Text = RuText;
          
            AgreeButton.Text = "Соглашаюсь";
           DisagreeButton.Text = "Не соглашаюсь";

        }

        private void RuClicked(object sender, EventArgs e)
        {
             EulaTextLabel.Text = RuText;
             AgreeButton.Text = "Соглашаюсь";
            DisagreeButton.Text = "Не соглашаюсь";
        }

        private void EnClicked(object sender, EventArgs e)
        {
            EulaTextLabel.Text = EnText;
            AgreeButton.Text = "Agree";
            DisagreeButton.Text = "Disagree";
        }

        private void AgreeClicked(object sender, EventArgs e)
        {
          DependencyService.Get<IEulaService>().Accept();
            Navigation.PopModalAsync();
        }

        private void DisagreeClicked(object sender, EventArgs e)
        {

            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }


        public string RuText = $@"Лицензионное соглашение с конечным пользователем («Соглашение»)
---------------------------------------------------------------

Последнее обновление: 19 января 2019 г.

Пожалуйста, внимательно прочтите настоящее Лицензионное соглашение с конечным пользователем («Соглашение»), прежде чем нажимать кнопку «Я согласен», загружать или использовать Doctor MaxWell («Приложение»).

Отказ от ответственности
------------------------

Информация на этом сайте не предназначена или не заменяет профессиональную медицинскую консультацию, диагностику или лечение. Вся информация, включая текст, графику, изображения и информацию, содержащиеся или доступные через эту программу или веб-сайт, предназначен только для общих информационных целей. Doctor MaxWell не делает никаких заявлений и не несет ответственности за точность информации, содержащейся на данном веб-сайте или доступной через него, и такая информация может быть изменена без предварительного уведомления. Вам рекомендуется подтверждать любую информацию, полученную с этого приложения или веб-сайта или через него, из других источников, а также просматривать всю информацию о любом состоянии здоровья или лечении у своего врача. НИКОГДА НЕ ПРЕНЕБРЕГАЙТЕ  ПРОФЕССИОНАЛЬНЫМИ МЕДИЦИНСКИМИ КОНСУЛЬТАЦИЯМИ И НЕ ОТКЛАДЫВАЙТЕ МЕДИЦИНСКУЮ ПОМОЩЬ, ИЗ-ЗА ТОГО, ЧТО ВЫ ЧТО-ТО ПРОЧИТАЛИ В ЭТОЙ ПРОГРАММЕ ИЛИ ВЕБ-САЙТЕ.

Doctor MaxWell не рекомендует, не одобряет и не делает никаких заявлений об эффективности, целесообразности или пригодности каких-либо конкретных тестов, продуктов, процедур, методов лечения, услуг, мнений, поставщиков медицинских услуг или другой информации, которая может содержаться в этом приложении или веб-сайте или доступным через него. DOCTOR MAXWELL НЕ ПРЕДСТАВЛЯЕТ И НЕ НЕСЕТ ОТВЕТСТВЕННОСТИ ЗА ЛЮБЫЕ КОНСУЛЬТАЦИИ, ПРОЦЕДУРУ ЛЕЧЕНИЯ, ДИАГНОСТИКУ ИЛИ ДРУГУЮ ИНФОРМАЦИЮ, УСЛУГИ ИЛИ ПРОДУКТЫ, КОТОРЫЕ ВЫ ПОЛУЧАЕТЕ В ЭТОМ ПРИЛОЖЕНИИ ИЛИ ВЕБ-САЙТЕ.

Doctor MaxWell не использует и не копирует какую-либо личную информацию, предоставленную вами или другими пользователями и / или организациями, не несет ответственности за доступ других пользователей к вашей информации. Doctor MaxWell использует только информацию, лицензированную для некоммерческого использования, без редактирования. Вся информация, которую вы можете получить от Doctor MaxWell, может быть получена без каких-либо ограничений с использованием бесплатного доступа в Интернет, Doctor MaxWell не редактирует и не сохраняет ее, поэтому не несет ответственности и не нарушает авторские права или лицензии.

Нажимая кнопку «Согласен», загружая или используя Приложение, вы соглашаетесь с условиями настоящего Соглашения.

Настоящее Соглашение является юридическим соглашением между вами (физическим или юридическим лицом) и компанией «Doctor MaxWell» и регулирует использование вами Приложения, предоставленного вам Doctor MaxWell.

Если вы не согласны с условиями настоящего Соглашения, не нажимайте кнопку «Я согласен» и не загружайте и не используйте Приложение.

Приложение Doctor MaxWell лицензировано бесплатно, а не продано вам для использования в строгом соответствии с условиями настоящего Соглашения.

Doctor MaxWell хранит всю информацию в Интернете на своих частных серверах и имеет права блокировать любого пользователя или пользователей, удалять приложение и любую информацию, включая всю информацию и онлайн-сервисы, без уведомления конечного пользователя.

Информация, содержащаяся на веб-сайте или в приложении «Doctor MaxWell» (далее «Сервис»), предназначена только для общего ознакомления. Доктор MaxWell не несет ответственности за ошибки или упущения в содержании Сервиса.

Ни при каких обстоятельствах Doctor MaxWell не несет ответственности за какие-либо особые, прямые, косвенные или случайные убытки или любые убытки, будь то в результате действия договора, халатности или другого правонарушения, возникшего в результате или в связи с использованием Сервиса. или содержание Сервиса. Доктор MaxWell оставляет за собой право в любое время добавлять, удалять или изменять содержимое Сервиса без предварительного уведомления. Doctor MaxWell не гарантирует, что сайт не содержит вирусов или других вредных компонентов.

Веб-сайт и приложение Doctor MaxWell могут содержать ссылки на внешние веб-сайты, которые не предоставляются или каким-либо образом связаны с Doctor MaxWell. Обратите внимание, что Doctor MaxWell не гарантирует точность, актуальность, своевременность или полноту любой информации на этих внешних веб-сайтах.

Лицензия
--------

Doctor MaxWell предоставляет вам отзывную, неисключительную, непередаваемую, ограниченную лицензию для загрузки, установки и использования Приложения исключительно в личных, некоммерческих целях в строгом соответствии с условиями настоящего Соглашения.

Сторонние сервисы
-----------------

Приложение может отображать, включать или предоставлять доступ к стороннему контенту (включая данные, информацию, приложения и услуги других продуктов) или предоставлять ссылки на сторонние веб-сайты или услуги («Сторонние услуги»).

Вы признаете и соглашаетесь с тем, что Doctor MaxWell не несет ответственности за какие-либо Услуги третьих лиц, включая их точность, полноту, своевременность, обоснованность, соблюдение авторских прав, законность, порядочность, качество или любые другие аспекты. Doctor MaxWell не несет и не несет никакой ответственности перед вами или любым другим физическим или юридическим лицом за какие-либо Услуги третьих лиц.

Сторонние сервисы и ссылки на них предоставляются исключительно для вашего удобства, и вы получаете к ним доступ и используете их исключительно на свой страх и риск и в соответствии с положениями и условиями таких третьих сторон.

Срок и прекращение действия
---------------------------

Настоящее Соглашение остается в силе до тех пор, пока не будет расторгнуто вами или Doctor MaxWell.

Doctor MaxWell может, по своему усмотрению, в любое время и по любой причине или без таковой приостановить или прекратить действие настоящего Соглашения с предварительным уведомлением или без него.

Настоящее Соглашение будет расторгнуто немедленно, без предварительного уведомления Doctor MaxWell, в случае, если вы не будете соблюдать какое-либо положение этого Соглашения. Вы также можете расторгнуть настоящее Соглашение, удалив Приложение и все его копии с вашего мобильного устройства или с вашего компьютера.

После прекращения действия настоящего Соглашения вы прекращаете любое использование Приложения и удаляете все копии Приложения со своего мобильного устройства или со своего компьютера.

Прекращение действия настоящего Соглашения не будет ограничивать какие-либо права или средства правовой защиты Doctor MaxWell в соответствии с законом или справедливостью в случае нарушения вами (в течение срока действия настоящего Соглашения) какого-либо из ваших обязательств по настоящему Соглашению.

Поправки к настоящему Соглашению
--------------------------------

Doctor MaxWell оставляет за собой право по своему усмотрению изменять или заменять настоящее Соглашение в любое время. Если пересмотр является существенным, мы предоставим уведомление по крайней мере за 60 дней до вступления в силу новых условий. Что представляет собой существенное изменение, будет определяться по нашему усмотрению.

Продолжая получать доступ к нашему Приложению или использовать его после вступления в силу любых изменений, вы соглашаетесь соблюдать пересмотренные условия. Если вы не согласны с новыми условиями, вы больше не имеете права использовать Приложение.

Управляющий закон
-----------------

Законодательство Российской Федерации, за исключением коллизионных норм, регулирует настоящее Соглашение и использование вами Приложения. Использование вами Приложения также может регулироваться другими местными, государственными, национальными или международными законами.

Контакты
--------

Если у вас есть какие-либо вопросы по поводу данного Соглашения, пожалуйста, свяжитесь с нами.

Полное согласие
---------------

Соглашение представляет собой полное соглашение между вами и Doctor MaxWell относительно использования вами Приложения и заменяет собой все предыдущие и существующие письменные или устные соглашения между вами и Doctor MaxWell.

На вас могут распространяться дополнительные условия, которые применяются при использовании или приобретении других услуг Doctor MaxWell, которые Doctor MaxWell предоставит вам во время такого использования или покупки.

";
        public string EnText = $@"End-User License Agreement (""Agreement"")  
----------------------------------------

Last updated: January 19, 2019

Please read this End-User License Agreement (""Agreement"") carefully before clicking the ""I Agree"" button, downloading or using Doctor MaxWell (""Application"").

Disclaimer
----------

The information on this site is not intended or implied to be a substitute for professional medical advice, diagnosis or treatment. All content, including text, graphics, images and information, contained on or available through this web site is for general information purposes only. Doctor MaxWell makes no representation and assumes no responsibility for the accuracy of information contained on or available through this web site, and such information is subject to change without notice. You are encouraged to confirm any information obtained from or through this web site with other sources, and review all information regarding any medical condition or treatment with your physician. NEVER DISREGARD PROFESSIONAL MEDICAL ADVICE OR DELAY SEEKING MEDICAL TREATMENT BECAUSE OF SOMETHING YOU HAVE READ ON OR ACCESSED THROUGH THIS WEB SITE.

Doctor MaxWell does not recommend, endorse or make any representation about the efficacy, appropriateness or suitability of any specific tests, products, procedures, treatments, services, opinions, health care providers or other information that may be contained on or available through this web site. DOCTOR MAXWELL IS NOT RESPONSIBLE NOR LIABLE FOR ANY ADVICE, COURSE OF TREATMENT, DIAGNOSIS OR ANY OTHER INFORMATION, SERVICES OR PRODUCTS THAT YOU OBTAIN THROUGH THIS WEB SITE.

Doctor MaxWell does not use, copy or distribute any private information provided by you or other users and/or organizations, is not responsible for other users able to access your information. Doctor MaxWell uses only the information licensed for non-commercial use without edit. All information you can get from Doctor MaxWell can be obtained without any restrictions using free internet access, Doctor MaxWell does not edit or save it, thus is not responsible and does not violate any copyrights or licenses.


By clicking the ""Agree"" button, downloading or using the Application, you are agreeing to be bound by the terms and conditions of this Agreement. 
This Agreement is a legal agreement between you (either an individual or a single entity) and Doctor MaxWell and it governs your use of the Application made  available to you by Doctor MaxWell.

If you do not agree to the terms of this Agreement, do not click on the ""I Agree"" button and do not download or use the Application.

The Application is licensed, not sold, to you by Doctor MaxWell for use strictly in accordance with the terms of this Agreement. 

Doctor MaxWell stores all the information online on it's private servers and has rights to block any user or users, remove the application and any information, including all of the information and online services, without notifying the end user.

The information contained on Doctor MaxWell website or application (the ""Service"") is for general information purposes only. Doctor MaxWell assumes no responsibility for errors or omissions in the contents on the Service.

In no event shall Doctor MaxWell be liable for any special, direct, indirect, consequential, or incidental damages or any damages whatsoever, whether in an action of contract, negligence or other tort, arising out of or in connection with the use of the Service or the contents of the Service. Doctor MaxWell reserves the right to make additions, deletions, or modification to the contents on the Service at any time without prior notice. Doctor MaxWell does not warrant that the website is free of viruses or other harmful components.

Doctor MaxWell website and application may contain links to external websites that are not provided or maintained by or in any way affiliated with Doctor MaxWell Please note that the Doctor MaxWell does not guarantee the accuracy, relevance, timeliness, or completeness of any information on these external websites.


License  
-------

Doctor MaxWell grants you a revocable, non-exclusive, non-transferable, limited license to download, install and use the Application solely for your personal, non-commercial purposes strictly in accordance with the terms of this Agreement.

Third-Party Services  
--------------------

The Application may display, include or make available third-party content (including data, information, applications and other products services) or provide links to third-party websites or services (""Third-Party Services"").

You acknowledge and agree that Doctor MaxWell shall not be responsible for any Third-Party Services, including their accuracy, completeness, timeliness, validity, copyright compliance, legality, decency, quality or any other aspect thereof. Doctor MaxWell does not assume and shall not have any liability or responsibility to you or any other person or entity for any Third-Party Services.

Third-Party Services and links thereto are provided solely as a convenience to you and you access and use them entirely at your own risk and subject to such third parties' terms and conditions.

Term and Termination  
--------------------

This Agreement shall remain in effect until terminated by you or Doctor MaxWell.

Doctor MaxWell may, in its sole discretion, at any time and for any or no reason, suspend or terminate this Agreement with or without prior notice.

This Agreement will terminate immediately, without prior notice from Doctor MaxWell, in the event that you fail to comply with any provision of this Agreement. You may also terminate this Agreement by deleting the Application and all copies thereof from your mobile device or from your computer. 

Upon termination of this Agreement, you shall cease all use of the Application and delete all copies of the Application from your mobile device or from your computer.

Termination of this Agreement will not limit any of Doctor MaxWell's rights or remedies at law or in equity in case of breach by you (during the term of this Agreement) of any of your obligations under the present Agreement.

Amendments to this Agreement  
----------------------------

Doctor MaxWell reserves the right, at its sole discretion, to modify or replace this Agreement at any time. If a revision is material we will provide at least 60 days' notice prior to any new terms taking effect. What constitutes a material change will be determined at our sole discretion.

By continuing to access or use our Application after any revisions become effective, you agree to be bound by the revised terms. If you do not agree to the new terms, you are no longer authorized to use the Application.

Governing Law  
-------------

The laws of Russian Federation, excluding its conflicts of law rules, shall govern this Agreement and your use of the Application. Your use of the Application may also be subject to other local, state, national, or international laws.

Contact Information  
-------------------

If you have any questions about this Agreement, please contact us.

Entire Agreement  
----------------

The Agreement constitutes the entire agreement between you and Doctor MaxWell regarding your use of the Application and supersedes all prior and contemporaneous written or oral agreements between you and Doctor MaxWell.

You may be subject to additional terms and conditions that apply when you use or purchase other Doctor MaxWell's services, which Doctor MaxWell will provide to you at the time of such use or purchase.
";


    }
}