package com.wechatauto.client;

/**
 * Exception thrown when an API call fails (non-2xx status) or the request could
 * not be completed (network/serialization error).
 *
 * <p>This is an unchecked exception so callers are not forced to declare it on
 * every method signature, but may catch it explicitly where error handling is
 * required.</p>
 */
public class WeChatAutoException extends RuntimeException {

    private final int statusCode;

    /**
     * @param statusCode HTTP status code. 0 is used for transport/serialization errors
     *                   where no HTTP response was received.
     * @param message    human readable message, includes response body on non-2xx.
     */
    public WeChatAutoException(int statusCode, String message) {
        super(message);
        this.statusCode = statusCode;
    }

    /**
     * @param statusCode HTTP status code. 0 is used for transport/serialization errors.
     * @param message    human readable message.
     * @param cause      underlying cause.
     */
    public WeChatAutoException(int statusCode, String message, Throwable cause) {
        super(message, cause);
        this.statusCode = statusCode;
    }

    /**
     * @return the HTTP status code, or 0 if no HTTP response was received.
     */
    public int getStatusCode() {
        return statusCode;
    }
}
