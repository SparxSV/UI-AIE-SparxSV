using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace UI_EventSystems
{
	public class BlockUI : MonoBehaviour
	{
		public Block block;

		[Header("Block Components")] 
		public Image icon;
		public TextMeshProUGUI nameTag;
		public TextMeshProUGUI descriptionTag;

		private Player player;

		private void Start()
		{
			SetBlock(block);
		}

		public void SetBlock(Block _b)
		{
			block = _b;
			
			if(nameTag)
				nameTag.text = block.actionName;

			if(descriptionTag)
				descriptionTag.text = block.description;

			if(icon)
			{
				icon.sprite = block.icon;
				icon.color = block.color;
			}
		}
		
		public void Init(Player _player)
		{
			player = _player;

			Button button = GetComponentInChildren<Button>();
			if(button)
				button.onClick.AddListener(() => { player.DoBlock(block); });
		}
	}
}