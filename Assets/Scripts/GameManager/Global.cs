using UnityEngine;

public static class Global {
    public static CustomizedDataSO customData;

    static Global () {
        customData = Resources.Load<CustomizedDataSO>("Data/CustomizedData");
    }
}
