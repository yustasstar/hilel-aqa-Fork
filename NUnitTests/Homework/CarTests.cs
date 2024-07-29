using NUnitTests.Lessons;

namespace NUnitTests.Homework
{
    public class CarTests : Lesson3Logic
    {
        //TODO: Finish car tests here or in Lesson3Logic file folowing example


        #region[TestCases]
        //TODO: TestCases

        //Test Case 1: Test Acceleration
        [Test, Description("Ensure that the method GetAcceleration correctly retrieves the current acceleration value.")]
        [Order(1)]
        public void TestAcceleration()
        {
            //Steps:
            //Initialize an instance of Lesson3Logic.
            //Call the GetAcceleration method.
            GetAcceleration();
            //Verify that the Acceleration property matches CurrentAcceleration.
            Assert.That(Acceleration, Is.EqualTo(CurrentAcceleration), "Acceleration is not equal to CurrentAcceleration");
        }

        //Test Case 2: Test GetSpeed with Positive Acceleration
        //Description: Verify that GetSpeed correctly assigns the current speed to the Speed property when the acceleration is positive.
        //Steps:
        [Test]
        [Order(2)]
        public void TestGetSpeed()
        {
            //Set CurrentAcceleration to a positive value.
            CurrentAcceleration = 50;
            //Call the GetSpeed method.
            GetSpeed();
            //Check that Speed equals CurrentSpeed.
            Assert.That(Speed, Is.EqualTo(CurrentSpeed));
        }

        //Test Case 3: Test GetDeceleration
        [Test, Description("Check if GetDeceleration correctly calculates deceleration as the difference between current speed and deceleration.")]
        [Order(3)]
        public void TestGetDeceleration()
        {
            //Steps:
            //Set a known value for CurrentSpeed and CurrentDeceleration.
            CurrentSpeed = MinSpeed;
            CurrentDeceleration = 0;
            //Invoke GetDeceleration.
            GetDeceleration();
            //Ensure Deceleration equals CurrentSpeed - CurrentDeceleration.
            Assert.That(Deceleration, Is.EqualTo(CurrentSpeed), "Deceleration is not equal to CurrentSpeed");
        }

        //Test Case 4: Speed Alert on Exceeding Max Speed
        //Description: Validate that SetSpeedAlert generates the correct alert when the speed exceeds the maximum speed.
        [Test]
        [Order(4)]
        public void TestSetSpeedAlert()
        {
            //Steps:
            //Set CurrentSpeed to exceed MaxSpeed.
            //Execute SetSpeedAlert.
            //Confirm that SpeedAlert contains the appropriate warning message.
            CurrentSpeed = MaxSpeed + 30;
            SetSpeedAlert(CurrentSpeed, MaxSpeed);
            Assert.That(Alert, Is.EqualTo("Take caution! Speed limit overdue " + (CurrentSpeed - MaxSpeed) + "!"));
        }


        //Test Case 5: Low Charge Alert
        [Test, Order(5), Description("Test SetChargeAlert for generating a low charge alert when charge falls below the critical level.")]
        public void LowChargeAlert()
        {
            //Steps:
            //Set Charge to just below CriticalCharge.
            Charge = CriticalCharge - 1;
            //Call SetChargeAlert.
            SetChargeAlert();
            //Check that SpeedAlert includes the low charge warning.
            Assert.That(Alert, Is.EqualTo("Take caution! Charge Low at " + Charge + "%!"), "SpeedAlert is not includes the low charge warning");
        }

        [Test]
        [Order(5)]
        public void LowChargeAlert2()
        {
            int charge = 8;
            Charge = charge;
            if (charge <= CriticalCharge)
            {
                SetChargeAlert();
            }
            Assert.That(Alert,Is.EqualTo("Take caution! Charge Low at " + Charge + "%!"));
        }

        //Test Case 6: Full Charge Alert
        [Test,Order(6), Description("Check that SetChargeAlert correctly alerts when charge exceeds critical overcharge level.")]
        public void FullChargeAlert()
        {
            //Steps:
            //Set Charge above CriticalOvercharge.
            Charge = CriticalOvercharge + 1;
            //Call SetChargeAlert.
            SetChargeAlert();
            //Verify that SpeedAlert warns about full charge and deceleration charge being disabled.
            Assert.That(Alert, Is.EqualTo("Charge Full! Deceleration charging disabled."), "SpeedAlert is not warns about full charge and deceleration charge being disabled");
        }


        //Test Case 7: Deceleration Charge Activation Safety
        //Description: Test the logic for enabling or disabling the deceleration charge feature based on the charge level.
        [Test]
        [Order(7)]
        public void TestDeceleration()
        {
            //Steps:
            //Set Charge below CriticalOvercharge.
            //Invoke DecelerationChargeActivation with isActive as true.
            //Confirm that IsDecelerationChargeActive is true.
            Charge = CriticalOvercharge - 2;
            DecelerationChargeActivation(true, CriticalOvercharge);
            Assert.That(IsDecelerationChargeActive);
        }


        //Test Case 8: Deceleration Charge Deactivation Safety
        [Test, Order(8), Description("Ensure that deceleration charging is disabled when charge exceeds the safe threshold.")]
        public void DecelerationChargeDeactivationSafety()
        {
            //Steps:
            //Set Charge above CriticalOvercharge.
            //Call DecelerationChargeActivation with isActive as true.
            DecelerationChargeActivation(true, CriticalOvercharge);
            //Ensure IsDecelerationChargeActive is false.
            Assert.That(IsDecelerationChargeActive, Is.False, "IsDecelerationChargeActive is true");
        }

        [Test]
        [Order(8)]

        public void DecelerationChargeDeactivation()
        {
            int charge = 101;
            Charge = charge;
            DecelerationChargeActivation(true, 98);
            Assert.That(IsDecelerationChargeActive,Is.EqualTo(false));
        }

        //Test Case 9: Compute Deceleration Charge Power When Active
        //Description: Validate that GetDecelerationChargePower computes the correct power when the feature is active.
        [Test]
        [Order(9)]
        public void TestGetDecelerationChargePower()
        {
            //Steps:
            //Ensure DecelerationChargeMode is true.
            //Call GetDecelerationChargePower with isActive set to true.
            //Check that the returned value equals CurrentSpeed - CurrentAcceleration.
            Assert.That(DecelerationChargeMode, Is.EqualTo(true));
            GetDecelerationChargePower(DecelerationChargeMode);
            Assert.That(DecelerationCharge, Is.EqualTo(CurrentSpeed - CurrentAcceleration));
        }

        //Test Case 10: Compute Deceleration Charge Power When Inactive
        [Test, Order(10), Description("Check that GetDecelerationChargePower returns 0 when the feature is not active")]
        public void ComputeDecelerationChargePowerWhenInactive()
        {
            //Steps:
            //Ensure DecelerationChargeMode is true.
            Assert.That(DecelerationChargeMode, Is.True, "DecelerationChargeMode is false");
            //Invoke GetDecelerationChargePower with isActive set to false.
            GetDecelerationChargePower(false);
            //Verify that the result is 0.
            Assert.That(DecelerationCharge, Is.Zero, "DecelerationCharge is not 0");
        }
        #endregion
    }
}
