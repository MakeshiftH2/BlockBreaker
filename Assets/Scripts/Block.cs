using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
  
  // config params
  [SerializeField] AudioClip breakSound;
  [SerializeField] GameObject blockSparklesVFX;
  [SerializeField] Sprite[] hitSprites;
	
	// cached reference
	Level level;
	
	// state variables
	[SerializeField] int timesHit; // TODO Only serialized for debug puposes.
	
	private void Start()
	{
	  CountBreakableBlocks();
	}
	
	private void CountBreakableBlocks()
	{
	  level = FindObjectOfType<Level>();
	  if (tag == "Breakable")
	  {
	   level.CountBlocks();
	  }
	}
	
	private void OnCollisionEnter2D()
	{
	  if ( tag == "Breakable")
	 {
	   HandleHit();
	 }
	}
	
	private void HandleHit()
	{
	  int maxHits = hitSprites.Length + 1;
	  timesHit++;
	   if (timesHit >= maxHits)
	   {
	     	   DestroyBlock();
	   }
	   else
	   {
	     ShowNextHitSprite();
	   }
	}
	
	private void ShowNextHitSprite()
	{
	  int spriteIndex = timesHit - 1;
	  if(hitSprites[spriteIndex] != null)
	  {
	    	  GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
	  }
	  else
	  {
	    Debug.LogError("Block sprite is missing from array" + gameObject.name);
	  }
	}
	
	private void DestroyBlock()
	{
	  PlayBlockDestroySFX();
	  Destroy(gameObject);
	  level.BlockDestroyed();
	  TriggerSparklesVFX();
	}
	
	private void PlayBlockDestroySFX()
	{
	  FindObjectOfType<GameStatus>().AddToScore();
	  AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
	}
	
	private void TriggerSparklesVFX()
	{
	  GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
	  Destroy (sparkles, 1f);
	}
}
