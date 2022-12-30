using System.Collections;
using System.Collections.Generic;
using HCB.Utilities;
using UnityEngine;

public class BallManager : Singleton<BallManager>
{
    [SerializeField] private int _maxLevel;
    private BallCreator _ballCreator;

    public BallCreator BallCreator => _ballCreator == null ? _ballCreator = GetComponent<BallCreator>() : _ballCreator;
    
    public List<Ball> _balls = new List<Ball>();
    
    public void AddBall(Ball ball)
    {
        if (!_balls.Contains(ball))
        {
            _balls.Add(ball);
            HCB.Core.EventManager.OnMergeCheck.Invoke(CanMerge());
        }
           
        
        
    }

    public void RemoveBall(Ball ball)
    {
            if(_balls.Contains(ball))
            {
                _balls.Remove(ball);
                HCB.Core.EventManager.OnMergeCheck.Invoke(CanMerge());
            }
    }
    
    public bool CanMerge()
    {
        if (_balls.Count >= 3)
        {
            for (int i = 1; i <= _maxLevel; i++)
            {
               int currentCount = 0;
               
               for (int j = 0; j < _balls.Count; j++)
               {
                   if (_balls[j].Level == i)
                   {
                       currentCount++;
                       if (currentCount >= 3)
                           return true;
                   }
               }
            }
            
            
        }
        return false;  
       
    }


    public void Merge()
    {
        for (int i = 1; i <= _maxLevel; i++)
        {
            int currentCount = 0;
            
            List<Ball> mergeableBalls = new List<Ball>();
            
            for (int j = 0; j < _balls.Count; j++)
            {
                if (_balls[j].Level == i)
                {
                    currentCount++;
                    mergeableBalls.Add(_balls[j]);
                    if (currentCount >= 3)
                    {
                        for (int k = 0; k < mergeableBalls.Count; k++)
                        {
                            Destroy(mergeableBalls[k].gameObject);
                        }
                        
                        HCB.Core.EventManager.OnCreateBallLevelUp.Invoke(i);
                    }
                        
                        
                }
            }
            
            
        }
    }
}
