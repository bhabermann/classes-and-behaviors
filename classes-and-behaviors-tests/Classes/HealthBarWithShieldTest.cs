using System;
using classes_and_behaviors.Classes;
using Xunit;
using Xunit.Sdk;

namespace classes_and_behaviors_tests
{
    public class HealthBarWithShieldTest
    {
        // Regras de negócio
        /*
         1. Os valores atuais de vida e escudo nunca podem ser menor que zero;
         2. Os valores atuais de vida e escudo nunca podem ser maior que seu valor máximo;
         3. Sempre que houver escudo, o escudo receberá 80% do dano, o excedente é reduzido da vida.
            3.1 Quando não existe mais escudo, todo o dano é retirado da vida.
         4. A função Heal() somente deve curar a vida.
         5. A função Restore() deve sempre carregar 100% do escudo.
         6. Se o escudo atingir o valor 0 ele "quebra" e não pode mais ser restaurado a menos que seja consertado com o FixShield()
         6. Se o valor da vida chegar a zero, o personagem morre e não pode mais ser curado ou ter seu escudo regenerado, 
            a menos que ele seja revivido. Quando o personagem é revivido, ele volta com vida e escudo a 10% cada.
        */

        public HealthBarWithShieldTest()
        {
            
        }

        [Theory]
        [InlineData(100, 100, 100, 0, 80, 20)]
        [InlineData(100, 100, 120, 0, 76, 4)]
        [InlineData(100, 100, 150, 0, 50, 0)]
        [InlineData(100, 100, 220, 0, 0, 0)]
        [InlineData(100, 100, 220, 50, 0, 0)]
        [InlineData(100, 0, 95, 0, 5, 0)]
        public void DamageAndHealTestCases(int initialLife, int initialShield, int damage, int heal, int expectedLife, int expetedShield)
        {
            var _healthBarWithShield = new HealthBarWithShield(initialLife, initialShield);

            _healthBarWithShield.Damage(damage);
            _healthBarWithShield.Heal(heal);

            Assert.Equal(expectedLife, _healthBarWithShield.Life);
            Assert.Equal(expetedShield, _healthBarWithShield.Shield);
        }

        [Theory]
        [InlineData(100, 100, 100, 80, 100)]
        [InlineData(100, 100, 120, 76, 100)]
        [InlineData(100, 100, 150, 50, 0)]
        [InlineData(100, 100, 220, 0, 0)]
        [InlineData(20, 100, 100, 0, 20)]
        public void DamageAndRestoreTestCases(int initialLife, int initialShield, int damage, int expectedLife, int expetedShield)
        {
            var _healthBarWithShield = new HealthBarWithShield(initialLife, initialShield);

            _healthBarWithShield.Damage(damage);
            _healthBarWithShield.Restore();

            Assert.Equal(expectedLife, _healthBarWithShield.Life);
            Assert.Equal(expetedShield, _healthBarWithShield.Shield);
        }

        //public void HealWithReviveTestCase()
        //{

        //}

        public void Heal_WithoutRevive_ShouldNotHeal()
        {

        }

        public void Restore_WithoutFixShields_ShouldNotRestore()
        {

        }
    }
}
