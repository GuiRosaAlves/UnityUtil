using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Only works with ortographic camera
public class CameraBounds : MonoBehaviour
{
    [SerializeField] private Transform _cameraBounds;
    private Vector2 _boundsSize;
    private Vector2 _boundsOffset;
    private float _left, _right, _top, _bottom;

    void Awake ()
    {
        if (_cameraBounds)
        {
            Sprite sprite = _cameraBounds.GetComponent<SpriteRenderer>().sprite;
            float pixelPerUnits = sprite.rect.width / sprite.bounds.size.x;
            CalculateSize(sprite, pixelPerUnits);
            RefreshBounds();
        }
        else
        {
            Debug.Log("The camera does not have boundaries!");
        }
    }
	
	void LateUpdate ()
    {
        if (_cameraBounds)
        {
            Vector3 v3 = transform.position;
            v3.x = Mathf.Clamp(v3.x, _left, _right);
            v3.y = Mathf.Clamp(v3.y, _bottom, _top);
            transform.position = v3;

            RefreshBounds();
        }
    }

    protected void CalculateSize(Sprite sprite, float pixelPerUnits)
    {
        _boundsSize = new Vector2(_cameraBounds.transform.localScale.x * sprite.texture.width / pixelPerUnits,
            _cameraBounds.transform.localScale.y * sprite.texture.height / pixelPerUnits);
        _boundsOffset = _cameraBounds.transform.position;
    }

    protected void RefreshBounds()
    {
        var vertExtent = Camera.main.orthographicSize;
        var horzExtent = vertExtent * Screen.width / Screen.height;

        _left = horzExtent - _boundsSize.x / 2.0f + _boundsOffset.x;
        _right = _boundsSize.x / 2.0f - horzExtent + _boundsOffset.x;
        _bottom = vertExtent - _boundsSize.y / 2.0f + _boundsOffset.y;
        _top = _boundsSize.y / 2.0f - vertExtent + _boundsOffset.y;
    }
}