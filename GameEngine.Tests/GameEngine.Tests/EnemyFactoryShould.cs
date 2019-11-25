using System;
using Xunit;

namespace GameEngine.Tests
{
    //trait used for filtering
    //can be put at class level or method level
    [Trait("Category", "Enemy")]
    public class EnemyFactoryShould
    {

        #region Type

        //type equals
        [Fact]
        public void CreateNormalEnemyByDefault()
        {
            EnemyFactory sut = new EnemyFactory();

            Enemy enemy = sut.Create("Zombie");

            Assert.IsType<NormalEnemy>(enemy);
        }

        //type does not equal
        //skips this test
        [Fact(Skip = "Don't need to run this")]
        public void CreateNormalEnemyByDefault_NotTypeExample()
        {
            EnemyFactory sut = new EnemyFactory();

            Enemy enemy = sut.Create("Zombie");

            Assert.IsNotType<DateTime>(enemy);
        }

        //type equals
        [Fact]
        public void CreateBossEnemy()
        {
            EnemyFactory sut = new EnemyFactory();

            Enemy enemy = sut.Create("Zombie King", true);

            Assert.IsType<BossEnemy>(enemy);
        }

        //assert and get casted type, then run test on casted type
        [Fact]
        public void CreateBossEnemy_CastReturnedTypeExample()
        {
            EnemyFactory sut = new EnemyFactory();

            Enemy enemy = sut.Create("Zombie King", true);

            //Assert and get cast result
            BossEnemy boss = Assert.IsType<BossEnemy>(enemy);

            //Additional asserts on typed object
            Assert.Equal("Zombie King", boss.Name);
        }

        //assert object derives from type
        [Fact]
        public void CreateBossEnemy_AssertAssignableTypes()
        {
            EnemyFactory sut = new EnemyFactory();

            Enemy enemy = sut.Create("Zombie King", true);

            Assert.IsAssignableFrom<Enemy>(enemy);
        }

        #endregion

        //assert objects are not the same instance
        //complimentary Assert.Same
        [Fact]
        public void CreateSeperateInstances()
        {
            EnemyFactory sut = new EnemyFactory();

            Enemy enemy1 = sut.Create("Zombie");
            Enemy enemy2 = sut.Create("Zombie");

            Assert.NotSame(enemy1, enemy2);
        }

        #region Exceptions

        //throws exception
        [Fact]
        public void NotAllowNullName()
        {
            EnemyFactory sut = new EnemyFactory();

            //Assert.Throws<ArgumentNullException>(() => sut.Create(null));
            //can pass in the parameter name
            Assert.Throws<ArgumentNullException>("name", () => sut.Create(null));
        }

        //throws exception
        //returns exception to assert against
        [Fact]
        public void OnlyAllowKingOrQueenBossEnemies()
        {
            EnemyFactory sut = new EnemyFactory();

            EnemyCreationException ex = Assert.Throws<EnemyCreationException>(() => sut.Create("Zombie", true));

            Assert.Equal("Zombie", ex.RequestedEnemyName);
        }

        #endregion
    }
}
