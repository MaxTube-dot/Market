using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.Extensions.Logging;

namespace Microsoft.eShopWeb.Infrastructure.Data;

public class CatalogContextSeed
{
    public static async Task SeedAsync(CatalogContext catalogContext,
        ILogger logger,
        int retry = 0)
    {
        var retryForAvailability = retry;
        try
        {
            if (catalogContext.Database.IsSqlServer())
            {
                catalogContext.Database.Migrate();
            }

            if (!await catalogContext.CatalogBrands.AnyAsync())
            {
                await catalogContext.CatalogBrands.AddRangeAsync(
                    GetPreconfiguredCatalogBrands());

                await catalogContext.SaveChangesAsync();
            }

            if (!await catalogContext.CatalogTypes.AnyAsync())
            {
                await catalogContext.CatalogTypes.AddRangeAsync(
                    GetPreconfiguredCatalogTypes());

                await catalogContext.SaveChangesAsync();
            }

            if (!await catalogContext.CatalogItems.AnyAsync())
            {
                await catalogContext.CatalogItems.AddRangeAsync(
                    GetPreconfiguredItems());

                await catalogContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            if (retryForAvailability >= 10) throw;

            retryForAvailability++;
            
            logger.LogError(ex.Message);
            await SeedAsync(catalogContext, logger, retryForAvailability);
            throw;
        }
    }

    static IEnumerable<CatalogBrand> GetPreconfiguredCatalogBrands()
    {
        return new List<CatalogBrand>
            {
          new("Bosch"),
            new("Makita"),
            new("DeWalt"),
            new("Stanley"),
            new("3M"),
            };
    }

    static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
    {
        return new List<CatalogType>
            {
                new("Инструменты"),
                new("Строительные материалы"),
                new("Строительная техника"),
                new("Защитное снаряжение")
            };
    }

    static IEnumerable<CatalogItem> GetPreconfiguredItems()
    {
        return new List<CatalogItem>
            {
                // Категория 1: Инструменты
                new(1,1, "Молоток", "Молоток для строительства", 15.99M, "https://avatars.mds.yandex.net/get-mpic/5269959/img_id3609033842006570034.jpeg/orig"),
                new(1,1, "Отвертка", "Отвертка с набором насадок", 12.50M, "https://tula.belydom.ru/upload/iblock/df0/phcr2kdpa16p8tc6eyohq838sp9c0fq2.webp"),
                new(1,1, "Пила", "Ручная пила по дереву", 18.75M, "https://avatars.mds.yandex.net/get-mpic/4397559/img_id4874550256306583118.jpeg/orig"),
                new(1,1, "Шуруповерт", "Беспроводной шуруповерт", 29.99M, "https://megamaster.ru/upload/iblock/0df/0dfa96081fd51a2f80bdfe46db6f2fbc.jpg"),
    
                // Категория 2: Строительные материалы
                new(2,2, "Кирпичи", "Красные кирпичи для строительства", 0.75M, "https://prorabich.ru/wp-content/uploads/2020/04/kirpich-smolensk.jpeg"),
                new(2,3, "Цемент", "Цемент для строительных работ", 12.99M, "https://st43.stpulscen.ru/images/product/484/027/600_big.jpg"),
                new(2,3, "Песок", "Строительный песок", 8.25M, "https://static.tildacdn.com/tild3131-6634-4536-a239-646130306364/773b5a74.jpeg"),
                new(2,4, "Гипсокартон", "Листы гипсокартона", 15.50M, "https://stroynel.ru/upload/iblock/da9/kmwjeo367ha0bdzwxdect3m58o5kfht9.jpg"),

                // Категория 3: Строительная техника
                new(3,5, "Бетоносмеситель", "Электрический бетоносмеситель", 299.99M, "https://mft24.ru/image/cache/catalog/YML6fc7e24a4910827e3ad5c3eb513676c1/betonosmesiteli/IMGc6970fcd28b1b3dea1bb98304cadb04a-1000x1000.jpg"),
                new(3,5, "Электропила", "Электрическая цепная пила", 129.50M, "https://benzo71.ru/upload/iblock/9cc/w2bi5rw0pcw0nxmb1ymrcj0ncgqrn3sw.jpg"),
                new(3,5, "Виброплита", "Бензиновая виброплита", 499.75M, "https://102tools.ru/upload/iblock/965/x9sekdam1pen4s3sa5i4aftn422n8jkl.jpeg"),
                new(3,5, "Бензопила", "Бензопила с двигателем мощностью 2.5 л.с.", 199.99M, "https://main-cdn.sbermegamarket.ru/hlr-system/-2/12/07/97/16/53/31/100026318705b0.jpg"),

                // Категория 4: Защитное снаряжение
                new(4,1, "Защитный шлем", "Шлем для строительных работ", 25.50M, "https://tornada.ru/images/stories/virtuemart/product/829-908_908_1-8297c2722b057bf2f95f2bc0b8cc74d8-1000x1000.png"),
                new(4,1, "Защитные перчатки", "Перчатки для защиты рук", 9.99M, "https://ktkrti.ru/img/icons/icon348_1654097125.jpg"),
                new(4,1, "Респиратор", "Респиратор сменных фильтров", 14.75M, "https://avatars.mds.yandex.net/get-mpic/5235429/img_id1928029919501693532.jpeg/orig"),
                new(4,1, "Защитные очки", "Очки для защиты глаз", 6.99M, "https://apline35.ru/upload/iblock/12c/e1ddx8tobodq63ci1xxmfgbvg06vrp9k/4e879c17db3511ec996100155d050400_4e879c2adb3511ec996100155d050400.jpg")
};
    }
}
