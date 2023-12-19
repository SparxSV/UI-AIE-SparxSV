using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Exercises.AIEExercise_Calculator
{
	public class ButtonType : MonoBehaviour
	{
		[SerializeField] private ButtonOperator buttonOperator;
		[SerializeField] private TextMeshProUGUI buttonText;
		
		private const float TEXT_SIZE = 100f;
		
		public enum ButtonOperator
		{
			Seven, Eight, Nine, Divide,
			Four, Five, Six, Times,
			One, Two, Three, Minus,
			Decimal, Zero, Equals, Plus,
			Clear
		}

		private void Start()
		{
		
			buttonText.fontSize = TEXT_SIZE;
			
			switch(buttonOperator)
			{
				case ButtonOperator.Seven:
					buttonText.text = "7";
					break;
				
				case ButtonOperator.Eight:
					buttonText.text = "8";
					break;
				
				case ButtonOperator.Nine:
					buttonText.text = "9";
					break;
				
				case ButtonOperator.Divide:
					buttonText.text = "/";
					break;
				
				case ButtonOperator.Four:
					buttonText.text = "4";
					break;
				
				case ButtonOperator.Five:
					buttonText.text = "5";
					break;
				
				case ButtonOperator.Six:
					buttonText.text = "6";
					break;
				
				case ButtonOperator.Times:
					buttonText.text = "x";
					break;
				
				case ButtonOperator.One:
					buttonText.text = "1";
					break;
				
				case ButtonOperator.Two:
					buttonText.text = "2";
					break;
				
				case ButtonOperator.Three:
					buttonText.text = "3";
					break;
				
				case ButtonOperator.Minus:
					buttonText.text = "-";
					break;
				
				case ButtonOperator.Decimal:
					buttonText.text = ".";
					break;
				
				case ButtonOperator.Zero:
					buttonText.text = "0";
					break;
				
				case ButtonOperator.Equals:
					buttonText.text = "=";
					break;
				
				case ButtonOperator.Plus:
					buttonText.text = "+";
					break;
				
				case ButtonOperator.Clear:
					buttonText.text = "C";
					break;
				
				default:
					Debug.LogWarning("Enum Error, Out of Range.");
					break;
			}
		}
	}
}