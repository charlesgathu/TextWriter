using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace TextReader.Managers.Test
{
    [TestClass]
    public class TestReaderManagerUnitTests
    {

        private IContainer container;
        private string[] paragraphs;

        [TestInitialize]
        public void Init()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TextReaderManager>().As<ITextReaderManager>();
            builder.RegisterType<WordCalculator>().As<IWordCalculator>();

            container = builder.Build();

            paragraphs = new string[] {
                "От Бессмертие ах препятства до Со Ни Да Злодействы осребряешь ею спустились На. Но Вы ты об до вы яр То. Тех Ним Сем нет жен. Искренних светлеясь боголюбив прославил. Ея Ея во ИЗ Тя Се об со. Защищаюсь Трисвятый бою причастны Исповемся Хранителя Арф Тот Надгробну Кои. Воздевая Всевидца поникнет Неверных помыслах. Дна вот ПРАВЕДНЫЙ Дуб Прощеньем дай хвалебный составляя чья порицаний.",
                "﻿кто. Лицемерья Исповемся достатком. Лес цел мню Как тут Дум уже ﻿Кто уст. Мая Вся сам Лук Нам. . Клоню любит Ничто. Князи сынов уха зло мир вовек лютый Летит миром Дол.",
                "За ль ИЗ мя ни Им. Веру Вера вере Сияй часу Хоть. Сокровищей стряхнутся развратный. То им Да мрак вдов то мы бурю бы Он суща Ко. Скрытный бедствах осталося здателем воцарить. Постыдится УПОВАЮЩЕМУ несказанна необъемлем милосердью.",
                ". Бывый обаял вреде слуху своим. Вас чем Нам шаг над. Пой быв они дан все. Мука влил лить злую Вы очах То об Понт из На уж. Паче Гром лавр. До ﻿Кто мы Да по. Их за вы Встань Ко Во узреть ко бы мрачат. ",
                "\n"
            };
        }

        [TestMethod]
        public void AcceptsOddCharactersTest()
        {
            //arrange
            var text = @"От Бессмертие ах препятства до Со Ни Да Злодействы осребряешь ею спустились На. ";

            using (var scope = container.BeginLifetimeScope())
            {
                var reader = scope.Resolve<ITextReaderManager>();
                //act
                var result = reader.Sort(text, SortOption.None);

                //assert
                Assert.AreEqual(text, result);
            }
        }

        [TestMethod]
        public void AcceptsParagraphsTest()
        {
            //arrange
            var text = string.Join('\n', paragraphs);
            var result = string.Join('\n', paragraphs.OrderBy(o => o));

            using (var scope = container.BeginLifetimeScope())
            {
                var reader = scope.Resolve<ITextReaderManager>();

                //act
                var textResult = reader.Sort(text, SortOption.Ascending);

                //assert
                Assert.AreEqual(result, textResult);
            }
        }

        [TestMethod]
        public void GetStatisticsTest()
        {
            //arrange
            var text = string.Join('\n', paragraphs);
            using (var scope = container.BeginLifetimeScope())
            {
                var reader = scope.Resolve<ITextReaderManager>();

                //act
                var statistics = reader.GetStatistics(text);

                //assert
                Assert.AreEqual(statistics.Hyphens, 0);
                Assert.AreEqual(statistics.Words, 176);
                Assert.AreEqual(statistics.Spaces, 175);
            }
        }

    }
}
