using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDefeatable{
    void Damage();
    void Destruction();
    IEnumerator FlashWhite();
    void BonusScoreDisplay();
    void OnTriggerEnter2D(Collider2D collision);
}
