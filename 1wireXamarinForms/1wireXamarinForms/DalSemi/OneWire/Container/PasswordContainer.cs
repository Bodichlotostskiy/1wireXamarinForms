﻿using System;
using System.Collections.Generic;
using System.Text;

namespace _1wireXamarinForms.DalSemi.OneWire.Container
{ /**
     * Public interface for all devices which implement some form of
     * password protection.  The operation protected could be reading from
     * the device, writing to the device, or both.  These interface methods
     * will allow you to set the passwords on the device, enable/disable the
     * passwords on the device, and set the passwords for the API to use
     * when interacting with the device.
     * 
     * @version    1.00, 8 Aug 2003
     * @author     shughes, JPE
     */

    public interface PasswordContainer
    {

        // -----------------------------------------------------------------

        /**
         * Returns the length in bytes of the Read-Only password.
         * 
         * @return the length in bytes of the Read-Only password.
         */
        int GetReadOnlyPasswordLength();

        /**
         * Returns the length in bytes of the Read/Write password.
         * 
         * @return the length in bytes of the Read/Write password.
         */
        int GetReadWritePasswordLength();

        /**
         * Returns the length in bytes of the Write-Only password.
         * 
         * @return the length in bytes of the Write-Only password.
         */
        int GetWriteOnlyPasswordLength();

        // -----------------------------------------------------------------

        /**
         * Returns the absolute address of the memory location where
         * the Read-Only password is written.
         * 
         * @return the absolute address of the memory location where
         *         the Read-Only password is written.
         */
        int GetReadOnlyPasswordAddress();

        /**
         * Returns the absolute address of the memory location where
         * the Read/Write password is written.
         * 
         * @return the absolute address of the memory location where
         *         the Read/Write password is written.
         */
        int GetReadWritePasswordAddress();

        /**
         * Returns the absolute address of the memory location where
         * the Write-Only password is written.
         * 
         * @return the absolute address of the memory location where
         *         the Write-Only password is written.
         */
        int GetWriteOnlyPasswordAddress();

        // -----------------------------------------------------------------

        /**
         * Returns true if this device has a Read-Only password.
         * If false, all other functions dealing with the Read-Only
         * password will throw an exception if called.
         * 
         * @return <code>true</code> if this device has a Read-Only password.
         */
        Boolean HasReadOnlyPassword();

        /**
         * Returns true if this device has a Read/Write password.
         * If false, all other functions dealing with the Read/Write
         * password will throw an exception if called.
         * 
         * @return <code>true</code> if this device has a Read/Write password.
         */
        Boolean HasReadWritePassword();

        /**
         * Returns true if this device has a Write-Only password.
         * If false, all other functions dealing with the Write-Only
         * password will throw an exception if called.
         * 
         * @return <code>true</code> if this device has a Write-Only password.
         */
        Boolean HasWriteOnlyPassword();

        // -----------------------------------------------------------------

        /**
         * Returns true if the device's Read-Only password has been enabled.
         * 
         * @return <code>true</code> if the device's Read-Only password has been enabled.
         */
        Boolean GetDeviceReadOnlyPasswordEnable();

        /**
         * Returns true if the device's Read/Write password has been enabled.
         * 
         * @return <code>true</code> if the device's Read/Write password has been enabled.
         */
        Boolean GetDeviceReadWritePasswordEnable();

        /**
         * Returns true if the device's Write-Only password has been enabled.
         * 
         * @return <code>true</code> if the device's Write-Only password has been enabled.
         */
        Boolean GetDeviceWriteOnlyPasswordEnable();

        // -----------------------------------------------------------------

        /**
         * Returns true if this device has the capability to enable one type of password
         * while leaving another type disabled.  i.e. if the device has Read-Only password
         * protection and Write-Only password protection, this method indicates whether or
         * not you can enable Read-Only protection while leaving the Write-Only protection
         * disabled.
         * 
         * @return <code>true</code> if the device has the capability to enable one type 
         *         of password while leaving another type disabled.
         */
        Boolean HasSinglePasswordEnable();

        /**
         * <p>Enables/Disables passwords for this Device.  This method allows you to 
         * individually enable the different types of passwords for a particular
         * device.  If <code>hasSinglePasswordEnable()</code> returns true,
         * you can selectively enable particular types of passwords.  Otherwise,
         * this method will throw an exception if all supported types are not
         * enabled.</p>
         * 
         * <p>For this to be successful, either write-protect passwords must be disabled,
         * or the write-protect password(s) for this container must be set and must match
         * the value of the write-protect password(s) in the device's register.</p>
         * 
         * @param enableReadOnly if <code>true</code> Read-Only passwords will be enabled.
         * @param enableReadWrite if <code>true</code> Read/Write passwords will be enabled.
         * @param enableWriteOnly if <code>true</code> Write-Only passwords will be enabled.
         */
        void SetDevicePasswordEnable(Boolean enableReadOnly,
                       Boolean enableReadWrite, Boolean enableWriteOnly);

        /**
         * <p>Enables/Disables passwords for this device.  If the part has more than one
         * type of password (Read-Only, Write-Only, or Read/Write), all passwords
         * will be enabled.  This function is equivalent to the following:
         *    <code> owc41.setDevicePasswordEnable(
         *                    owc41.hasReadOnlyPassword(), 
         *                    owc41.hasReadWritePassword(),
         *                    owc41.hasWriteOnlyPassword() ); </code></p>
         * 
         * <p>For this to be successful, either write-protect passwords must be disabled,
         * or the write-protect password(s) for this container must be set and must match
         * the value of the write-protect password(s) in the device's register.</p>
         * 
         * @param enableAll if <code>true</code>, all passwords are enabled.  Otherwise,
         *        all passwords are disabled.
         */
        void SetDevicePasswordEnableAll(Boolean enableAll);

        // -----------------------------------------------------------------

        /**
         * <p>Writes the given password to the device's Read-Only password register.  Note
         * that this function does not enable the password, just writes the value to
         * the appropriate memory location.</p>
         * 
         * <p>For this to be successful, either write-protect passwords must be disabled,
         * or the write-protect password(s) for this container must be set and must match
         * the value of the write-protect password(s) in the device's register.</p>
         * 
         * @param password the new password to be written to the device's Read-Only
         *        password register.  Length must be 
         *        <code>(offset + getReadOnlyPasswordLength)</code>
         * @param offset the starting point for copying from the given password array
         */
        void SetDeviceReadOnlyPassword(byte[] password, int offset);

        /**
         * <p>Writes the given password to the device's Read/Write password register.  Note
         * that this function does not enable the password, just writes the value to
         * the appropriate memory location.</p>
         * 
         * <p>For this to be successful, either write-protect passwords must be disabled,
         * or the write-protect password(s) for this container must be set and must match
         * the value of the write-protect password(s) in the device's register.</p>
         * 
         * @param password the new password to be written to the device's Read-Write
         *        password register.  Length must be 
         *        <code>(offset + getReadWritePasswordLength)</code>
         * @param offset the starting point for copying from the given password array
         */
        void SetDeviceReadWritePassword(byte[] password, int offset);

        /**
         * <p>Writes the given password to the device's Write-Only password register.  Note
         * that this function does not enable the password, just writes the value to
         * the appropriate memory location.</p>
         * 
         * <p>For this to be successful, either write-protect passwords must be disabled,
         * or the write-protect password(s) for this container must be set and must match
         * the value of the write-protect password(s) in the device's register.</p>
         * 
         * @param password the new password to be written to the device's Write-Only
         *        password register.  Length must be 
         *        <code>(offset + getWriteOnlyPasswordLength)</code>
         * @param offset the starting point for copying from the given password array
         */
        void SetDeviceWriteOnlyPassword(byte[] password, int offset);

        // -----------------------------------------------------------------

        /**
         * Sets the Read-Only password used by the API when reading from the
         * device's memory.  This password is not written to the device's
         * Read-Only password register.  It is the password used by the
         * software for interacting with the device only.
         * 
         * @param password the new password to be used by the API when 
         *        reading from the device's memory.  Length must be 
         *        <code>(offset + getReadOnlyPasswordLength)</code>
         * @param offset the starting point for copying from the given password array
         */
        void SetContainerReadOnlyPassword(byte[] password, int offset);

        /**
         * Sets the Read/Write password used by the API when reading from  or
         * writing to the device's memory.  This password is not written to 
         * the device's Read/Write password register.  It is the password used 
         * by the software for interacting with the device only.
         * 
         * @param password the new password to be used by the API when 
         *        reading from or writing to the device's memory.  Length must be 
         *        <code>(offset + getReadWritePasswordLength)</code>
         * @param offset the starting point for copying from the given password array
         */
        void SetContainerReadWritePassword(byte[] password, int offset);

        /**
         * Sets the Write-Only password used by the API when writing to the
         * device's memory.  This password is not written to the device's
         * Write-Only password register.  It is the password used by the
         * software for interacting with the device only.
         * 
         * @param password the new password to be used by the API when 
         *        writing to the device's memory.  Length must be 
         *        <code>(offset + getWriteOnlyPasswordLength)</code>
         * @param offset the starting point for copying from the given password array
         */
        void SetContainerWriteOnlyPassword(byte[] password, int offset);

        // -----------------------------------------------------------------

        /**
         * Returns true if the password used by the API for reading from the
         * device's memory has been set.  The return value is not affected by 
         * whether or not the read password of the container actually matches 
         * the value in the device's password register.
         * 
         * @return <code>true</code> if the password used by the API for 
         * reading from the device's memory has been set.
         */
        Boolean IsContainerReadOnlyPasswordSet();

        /**
         * Returns true if the password used by the API for reading from or
         * writing to the device's memory has been set.  The return value is 
         * not affected by whether or not the read/write password of the 
         * container actually matches the value in the device's password 
         * register.
         * 
         * @return <code>true</code> if the password used by the API for 
         * reading from or writing to the device's memory has been set.
         */
        Boolean IsContainerReadWritePasswordSet();

        /**
         * Returns true if the password used by the API for writing to the
         * device's memory has been set.  The return value is not affected by 
         * whether or not the write password of the container actually matches 
         * the value in the device's password register.
         * 
         * @return <code>true</code> if the password used by the API for 
         * writing to the device's memory has been set.
         */
        Boolean IsContainerWriteOnlyPasswordSet();

        // -----------------------------------------------------------------

        /**
         * Gets the Read-Only password used by the API when reading from the
         * device's memory.  This password is not read from the device's
         * Read-Only password register.  It is the password used by the
         * software for interacting with the device only and must have been
         * set using the <code>setContainerReadOnlyPassword</code> method.
         * 
         * @param password array for holding the password that is used by the 
         *        API when reading from the device's memory.  Length must be 
         *        <code>(offset + getWriteOnlyPasswordLength)</code>
         * @param offset the starting point for copying into the given password array
         */
        void GetContainerReadOnlyPassword(byte[] password, int offset);

        /**
         * Gets the Read/Write password used by the API when reading from or 
         * writing to the device's memory.  This password is not read from 
         * the device's Read/Write password register.  It is the password used 
         * by the software for interacting with the device only and must have 
         * been set using the <code>setContainerReadWritePassword</code> method.
         * 
         * @param password array for holding the password that is used by the 
         *        API when reading from or writing to the device's memory.  Length must be 
         *        <code>(offset + getReadWritePasswordLength)</code>
         * @param offset the starting point for copying into the given password array
         */
        void GetContainerReadWritePassword(byte[] password, int offset);

        /**
         * Gets the Write-Only password used by the API when writing to the
         * device's memory.  This password is not read from the device's
         * Write-Only password register.  It is the password used by the
         * software for interacting with the device only and must have been
         * set using the <code>setContainerWriteOnlyPassword</code> method.
         * 
         * @param password array for holding the password that is used by the 
         *        API when writing to the device's memory.  Length must be 
         *        <code>(offset + getWriteOnlyPasswordLength)</code>
         * @param offset the starting point for copying into the given password array
         */
        void GetContainerWriteOnlyPassword(byte[] password, int offset);

        // -----------------------------------------------------------------

    }
}
