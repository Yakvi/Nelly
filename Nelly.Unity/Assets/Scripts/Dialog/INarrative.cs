using UnityEngine;

public interface INarrative {
    Slide GetNextSlide();
    INarrative GetNextUnit();
    void Reset();
}
