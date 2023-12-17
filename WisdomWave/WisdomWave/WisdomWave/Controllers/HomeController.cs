using System.Diagnostics;
using BLL.Services;
using Domain.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WisdomWave.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SubscriptionService _subscriptionService;
    private readonly CategoryService _categoryService;
    private readonly CourseService _courseService;
    private readonly UserManager<WwUser> _userManager;
    public HomeController(ILogger<HomeController> logger, SubscriptionService subscriptionService, CourseService courseService, UserManager<WwUser> userManager, CategoryService categoryService)
    {
        _subscriptionService = subscriptionService;
        _logger = logger;
        _courseService = courseService;
        _userManager = userManager;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {


        await _categoryService.CreateAsync(new Category { CategoryName = "Розробка" });

        Category parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Розробка");
        await _categoryService.CreateAsync(new Category { CategoryName = "Веб розробка" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Обробка та аналіз даних" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Розробка мобільних додатків" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Розробка ігор" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Проектування та разробка баз данних" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Інженерія розробки ПЗ" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Мови програмування" }, parentCategory.Id);

        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Веб розробка");
        await _categoryService.CreateAsync(new Category { CategoryName = "JavaScript" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "ReactJS" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "CSS" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Node.Js" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "ASP.NET Core" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Angular" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Python" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "TypeScript" }, parentCategory.Id);




        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Обробка та аналіз даних");
        await _categoryService.CreateAsync(new Category { CategoryName = "Python" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "AI" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "DeepLearning" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "ML" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "LangChain" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Python" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Rust" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Аналіз Даних" }, parentCategory.Id);




        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Розробка мобільних додатків");
        await _categoryService.CreateAsync(new Category { CategoryName = "Gooogle Flutter" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Розробка на IOS" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Розробка на Android" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "React Native" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Dart" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Swift" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Kotlin" }, parentCategory.Id);





        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Розробка ігор");
        await _categoryService.CreateAsync(new Category { CategoryName = "Unreal Engine" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Unity" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "C#" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "C++" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Розробка 2D ігор" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Розробка 3D ігор" }, parentCategory.Id);






        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Проектування та разробка баз данних");
        await _categoryService.CreateAsync(new Category { CategoryName = "SQL" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "MySql" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "SqlServer" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "PostgreSql" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "MongoDB" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Oracle SQL" }, parentCategory.Id);





        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Інженерія розробки ПЗ");
        await _categoryService.CreateAsync(new Category { CategoryName = "Структура даних" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Алгоритми" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Технічні співбесіди" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Архітектура ПЗ" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "JAVA" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Мікросервіси" }, parentCategory.Id);




        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Мови програмування");
        await _categoryService.CreateAsync(new Category { CategoryName = "Python" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "JAVA" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "C#" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "C++" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "JavaScript" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "ReactJS" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Rust" }, parentCategory.Id);

        ////////////////////////////////////////////////////////////////////////////////////

        await _categoryService.CreateAsync(new Category { CategoryName = "Бізнес" });


        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Бізнес");
        await _categoryService.CreateAsync(new Category { CategoryName = "Підприємництво" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Менеджмент" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Продажі" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Управління проектами" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Промисловість" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Інше про бізнес" }, parentCategory.Id);


        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Підприємництво");
        await _categoryService.CreateAsync(new Category { CategoryName = "Основи бізнесу" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Основи підприємництва" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Фріланс" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "ChatGPT" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Бізнес-стратегії" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Стартап" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Бізнес планування" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Онлайн-бізнес" }, parentCategory.Id);





        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Менеджмент");
        await _categoryService.CreateAsync(new Category { CategoryName = "Управління продукцією" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Лідерство" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Навички управління" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "ISO 9001" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Управління проектами" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Управління якістю" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Навчання менеджерів" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Управління ризиками" }, parentCategory.Id);





        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Продажі");
        await _categoryService.CreateAsync(new Category { CategoryName = "Навички продажу" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Продаж В2В" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "LinkedIn" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Сфера обслуговування" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Розвиток бізнесу" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Холодні листи" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Холодне продзвонювання" }, parentCategory.Id);




        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Управління проектами");
        await _categoryService.CreateAsync(new Category { CategoryName = "PMP" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "PMI PMBOK" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Сертифікований спеціаліст РМІ з управління проектами (САРМ)" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Scrum" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Професійний Scrum Master (PSM)" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Право власності на продукцію" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "PMI Agile Certified Practitioner (PMI-ACP)" }, parentCategory.Id);





        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Промисловість");
        await _categoryService.CreateAsync(new Category { CategoryName = "Прокладання труб" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Нафтогазова промисловість" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Хімічна технологія" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Безпека та охорона праці" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Електротехніка" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Авіація" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "EPLAN Electric P8" }, parentCategory.Id);




        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Інше про бізнес");
        await _categoryService.CreateAsync(new Category { CategoryName = "QuickBooks онлайн" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Написання заявок на грант" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Навички введення даних" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Транскрипція" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Цифрова трансформація" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Авіація" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Четверта промислова революція" }, parentCategory.Id);

        ////////////////////////////////////////////////////////////////////////////////////

        await _categoryService.CreateAsync(new Category { CategoryName = "Офісне програмне забезпечення" });

        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Офісне програмне забезпечення");
        await _categoryService.CreateAsync(new Category { CategoryName = "Microsoft" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Apple" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Google" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Інше про офісні програми" }, parentCategory.Id);

        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Microsoft");
        await _categoryService.CreateAsync(new Category { CategoryName = "Excel" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Microsoft 365 (Office)" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Excel VBA" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Microsoft Power BI" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "PowerPoint" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Формули та функції Ехсеl" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Аналіз даних" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "SharePoint" }, parentCategory.Id);




        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Apple");
        await _categoryService.CreateAsync(new Category { CategoryName = "Основи Мас" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "iMovie" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Apple Keynote" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "macOS" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Numbers для Мас" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Основи продуктів Apple" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Mac Pages" }, parentCategory.Id);




        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Google");
        await _categoryService.CreateAsync(new Category { CategoryName = "Google Таблиці" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Google Workspace (G Suite)" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Google AppSheet" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Google Apps Script" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Google Looker" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Google Диск" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Продуктивність Gmail" }, parentCategory.Id);



        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Інше про офісні програми");
        await _categoryService.CreateAsync(new Category { CategoryName = "SAP" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Oracle" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "ServiceNow" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Робочий простір Notion" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Автоматизація" }, parentCategory.Id);

        ////////////////////////////////////////////////////////////////////////////////////

        await _categoryService.CreateAsync(new Category { CategoryName = "Дизайн" });

        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Дизайн");
        await _categoryService.CreateAsync(new Category { CategoryName = "Веб дизайн" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Графічний дизайн та ілюстрація" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Дизайн ігор" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "3D та анімація" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Модний дизайн" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Архітектурне проектування" }, parentCategory.Id);

        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Веб дизайн");
        await _categoryService.CreateAsync(new Category { CategoryName = "Дизайн мобільних додатків" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "WordPress" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "CSS" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Figma" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Розробка інтерфейсу користувача" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Elementor" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Adobe Photoshop" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Webflow" }, parentCategory.Id);





        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Графічний дизайн та ілюстрація");
        await _categoryService.CreateAsync(new Category { CategoryName = "Графічний дизайн" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Adobe Photoshop" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Adobe Illustrator" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Canva" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Procreate" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Adobe InDesign" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Теорія дизайну" }, parentCategory.Id);



        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Дизайн ігор");
        await _categoryService.CreateAsync(new Category { CategoryName = "Піксель-арт" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Unreal Engine" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Unity" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Розробка ігор на ROBLOX" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Blender" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Дизайн рівнів" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Візуальні ефекти" }, parentCategory.Id);





        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "3D та анімація");
        await _categoryService.CreateAsync(new Category { CategoryName = "Blender" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "3D-моделювання" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Adobe After Effects" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "zBrush" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Анімація, розваги 3D" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Fusion 360" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Maya" }, parentCategory.Id);



        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Модний дизайн");
        await _categoryService.CreateAsync(new Category { CategoryName = "Мода" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "3D-дизайн у сфері моди" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Створення шаблонів (мода)" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Шиття" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Дизайн ювелірних виробів" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Adobe Illustrator" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Текстильна промисловість" }, parentCategory.Id);



        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Архітектурне проектування");
        await _categoryService.CreateAsync(new Category { CategoryName = "Revit" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "AutoCAD" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "LEED" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "SketchUp" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "ARCHICAD" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Building Information Modeling (BIM)" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Програмне забезпечення CAD" }, parentCategory.Id);

        ////////////////////////////////////////////////////////////////////////////////////

        await _categoryService.CreateAsync(new Category { CategoryName = "Маркетинг" });

        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Маркетинг");
        await _categoryService.CreateAsync(new Category { CategoryName = "Цифровий маркетинг" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Фірмовий стиль" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Платна реклама" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Зв'язки з громадськістю" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Товарний маркетинг" }, parentCategory.Id);

        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Цифровий маркетинг");
        await _categoryService.CreateAsync(new Category { CategoryName = "Маркетинг у соціальних мережах" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Маркетингова стратегія" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Стартап" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Копірайтінг" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Google Analytics" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Інтернет маркетинг" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Воронка продажів" }, parentCategory.Id);



        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Фірмовий стиль");
        await _categoryService.CreateAsync(new Category { CategoryName = "Бізнес брендинг" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Розширення аудиторії на YouTube" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Просування на YouTube" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Персональний брендінг" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Управління брендом" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Маркетингова стратегія" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Книжковий маркетинг" }, parentCategory.Id);




        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Платна реклама");
        await _categoryService.CreateAsync(new Category { CategoryName = "Google Реклама" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "РРС-реклама" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Сертифікація по Google Рекламі" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Реклама на Facebook" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Просування на YouTube" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Переорієнтація" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Рекламна стратегія" }, parentCategory.Id);



        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Зв'язки з громадськістю");
        await _categoryService.CreateAsync(new Category { CategoryName = "Навички комунікації" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Підготовка для роботи в галузі ЗМІ" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Створення подкастів" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Планування заходів" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Ділове спілкування" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Сфера обслуговування" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Управління споживчим досвідом" }, parentCategory.Id);



        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Товарний маркетинг");
        await _categoryService.CreateAsync(new Category { CategoryName = "Маркетинговий план" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Amazon Kindle Direct Publishing (KDP)" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Управління продукцією" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Маркетингова стратегія" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Продаж В2В" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Сфера обслуговування" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Управління маркетингом" }, parentCategory.Id);

        ////////////////////////////////////////////////////////////////////////////////////

        await _categoryService.CreateAsync(new Category { CategoryName = "Світлина та відео" });

        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Світлина та відео");
        await _categoryService.CreateAsync(new Category { CategoryName = "Цифрова фотографія" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Портрена фотографія" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Комерційна фотографія" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Дизайн відео" }, parentCategory.Id);

        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Цифрова фотографія");
        await _categoryService.CreateAsync(new Category { CategoryName = "Світлина" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Функції цифрової камери" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Мобільна фотографія" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Affinity Photo" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Adobe Lightroom" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "GIMP" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Adobe Photoshop" }, parentCategory.Id);



        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Портрена фотографія");
        await _categoryService.CreateAsync(new Category { CategoryName = "Позування" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Освітлення у фотографії" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Ретушування у Photoshop" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Midjourney" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Сімейна фотографія" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Комерційна фотографія" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Adobe Photoshop" }, parentCategory.Id);




        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Комерційна фотографія");
        await _categoryService.CreateAsync(new Category { CategoryName = "Фотографія нерухомості" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Архітектурна фотографія" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Весільна фотографія" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Ретушування у Photoshop" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Фотозйомка продукції" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Світлина їжі" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Освітлення у фотографії" }, parentCategory.Id);



        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Дизайн відео");
        await _categoryService.CreateAsync(new Category { CategoryName = "Відеомонтаж" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Adobe Premiere" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "DaVinci Resolve" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Adobe After Effects" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Unreal Engine" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Кіновиробництво" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Корекція кольору" }, parentCategory.Id);

        ////////////////////////////////////////////////////////////////////////////////////

        await _categoryService.CreateAsync(new Category { CategoryName = "Музика" });


        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Музика");
        await _categoryService.CreateAsync(new Category { CategoryName = "Музичне виробництво" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Основи музики" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Вокал" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Інше про музику" }, parentCategory.Id);


        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Музичне виробництво");
        await _categoryService.CreateAsync(new Category { CategoryName = "FL Studio" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Logic Pro" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Ableton Live" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Зведення музики" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Музикальна композиція" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Звукорежисура" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Аудіовиробництво" }, parentCategory.Id);

        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Основи музики");
        await _categoryService.CreateAsync(new Category { CategoryName = "Теорія музики" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Музикальна композиція" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Твір пісень" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Електронна музика" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Нотна грамотність" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "ABRSM" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Гармонія (музика)" }, parentCategory.Id);

        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Вокал");
        await _categoryService.CreateAsync(new Category { CategoryName = "Спів" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Постановка голосу" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Озвучка" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Музика у стилі \"рага\"" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Медитація" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Pen" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Озвучення та закадровий переклад" }, parentCategory.Id);

        parentCategory = await _categoryService.FindByConditionItemAsync(c => c.CategoryName == "Інше про музику");
        await _categoryService.CreateAsync(new Category { CategoryName = "DJ" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Музичний бізнес" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Твір пісень" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Pen" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Музичний маркетинг" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Звукова терапія" }, parentCategory.Id);
        await _categoryService.CreateAsync(new Category { CategoryName = "Джембе" }, parentCategory.Id);


        return Redirect("http://localhost:3000/");

    }
  
 

}
