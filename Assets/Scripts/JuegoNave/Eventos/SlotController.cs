using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class SlotController : MonoBehaviour
{
    public SlotScroller[] slots;
    private int indiceActual = 0;

    [System.Serializable]
    public class WinCondition
    {
        public string victoriaNombre;
        public UnityEvent victoria;
        public int[] ganacionPatron;
    }

    public WinCondition[] condicionesVic;

    public UnityEvent derrota;

    private void OnEnable()
    {
        ResetSlots();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (indiceActual < slots.Length)
            {
                slots[indiceActual].Stop();
                indiceActual++;

                if (indiceActual == slots.Length)
                {
                    CheckWinCondicion();
                }
            }
        }
    }

    public int[] GetIndicesFinales()
    {
        int[] selectedIndexes = new int[slots.Length];
        for (int i = 0; i < slots.Length; i++)
        {
            selectedIndexes[i] = slots[i].GetIndiceSelecionado();
        }
        return selectedIndexes;
    }

    private void CheckWinCondicion()
    {
        int[] selectedIndexes = GetIndicesFinales();

        foreach (var condition in condicionesVic)
        {
            if (MatchEnPatron(selectedIndexes, condition.ganacionPatron))
            {
                condition.victoria.Invoke();
                StartCoroutine(TerminarSlots());
                return;
            }
        }
        derrota.Invoke();
    }

    private bool MatchEnPatron(int[] selectedIndexes, int[] pattern)
    {
        if (selectedIndexes.Length != pattern.Length)
            return false;

        for (int i = 0; i < pattern.Length; i++)
        {
            if (selectedIndexes[i] != pattern[i])
                return false;
        }
        return true;
    }

    public void ResetSlots()
    {
        indiceActual = 0;

        foreach (SlotScroller slot in slots)
        {
            slot.ResetSlot();
        }
    }

    private IEnumerator TerminarSlots()
    {
        yield return new WaitForSeconds(2);
        derrota.Invoke();
    }
}